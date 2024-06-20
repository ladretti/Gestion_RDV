using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gestion_RDV.Models.EntityFramework;
using AutoMapper;
using Gestion_RDV.Models.DTO;
using Gestion_RDV.Models.Repository;
using Microsoft.Extensions.Hosting;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;

namespace Gestion_RDV.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturesController : ControllerBase
    {
        private readonly IDataRepository<Facture> dataRepository;
        private readonly IDataRepository<RendezVous> dataRepositoryRendezVous;
        private readonly IMapper _mapper;
        //private readonly IDataRepositoryUser<User> dataRepositoryUser;

        public FacturesController(IDataRepository<Facture> dataRepo, IMapper mapper, IDataRepository<RendezVous> dataRepoRendezVous)
        {
            dataRepository = dataRepo;
            _mapper = mapper;
            dataRepositoryRendezVous = dataRepoRendezVous;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<FactureDTO>> GetFactureById(int id)
        {
            var like = await dataRepository.GetByIdAsync(id);
            await dataRepositoryRendezVous.GetAllAsync();

            if (like.Value == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<FactureDTO>(like.Value));
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<FactureDTO>> PostFacture(FacturePostDTO facture)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Facture facture1 = _mapper.Map<Facture>(facture);
            await dataRepository.AddAsync(facture1);

            return CreatedAtAction(nameof(GetFactureById), new { id = facture1.FactureId }, _mapper.Map<FactureDTO>(facture1));
        }
        [HttpGet("generate-pdf/{id}")]
        public async Task<IActionResult> GeneratePdf(int id)
        {
            var facture = await dataRepository.GetByIdAsync(id);
            if (facture == null)
            {
                return NotFound();
            }

            var rendezVous = await dataRepositoryRendezVous.GetByIdAsync(facture.Value.RendezVousId);
            if (rendezVous == null)
            {
                return NotFound();
            }

            using (var document = new PdfDocument())
            {
                var page = document.AddPage();
                var gfx = XGraphics.FromPdfPage(page);
                var font = new XFont("Verdana", 12, XFontStyle.Regular);
                var titleFont = new XFont("Verdana", 14, XFontStyle.Bold);
                var boldFont = new XFont("Verdana", 12, XFontStyle.Bold);



                gfx.DrawImage(XImage.FromFile("Images/logo.jpg"), 20, 20, 100, 100);
                gfx.DrawString("Facture", titleFont, XBrushes.Black, new XRect(20, 120, page.Width, page.Height), XStringFormats.TopLeft);

                gfx.DrawString($"Facture ID: {facture.Value.FactureId}", font, XBrushes.Black, new XRect(20, 150, page.Width, page.Height), XStringFormats.TopLeft);
                gfx.DrawString($"Date: {rendezVous.Value.EndDate:dd-MM-yyyy}", font, XBrushes.Black, new XRect(20, 170, page.Width, page.Height), XStringFormats.TopLeft);
                gfx.DrawString($"Rendez-vous ID: {rendezVous.Value.RendezVousId}", font, XBrushes.Black, new XRect(20, 190, page.Width, page.Height), XStringFormats.TopLeft);
                gfx.DrawString($"Description: {rendezVous.Value.Description}", font, XBrushes.Black, new XRect(20, 210, page.Width, page.Height), XStringFormats.TopLeft);
                gfx.DrawString($"Informations: {facture.Value.Informations}", font, XBrushes.Black, new XRect(20, 230, page.Width, page.Height), XStringFormats.TopLeft);

                decimal prixAvantTva = facture.Value.PrixAvantTva;
                decimal tva = facture.Value.Tva;
                decimal montantTva = prixAvantTva * tva / 100;
                decimal prixTotal = prixAvantTva + montantTva;

                int tableStartY = 260;
                int rowHeight = 20;

                gfx.DrawString("Détail du calcul de la TVA", titleFont, XBrushes.Black, new XRect(20, tableStartY, page.Width, page.Height), XStringFormats.TopLeft);

                gfx.DrawString("Description", font, XBrushes.Black, new XRect(20, tableStartY + rowHeight, page.Width, page.Height), XStringFormats.TopLeft);
                gfx.DrawString("Montant (EUR)", font, XBrushes.Black, new XRect(200, tableStartY + rowHeight, page.Width, page.Height), XStringFormats.TopLeft);

                gfx.DrawString("Prix avant TVA", font, XBrushes.Black, new XRect(20, tableStartY + 2 * rowHeight, page.Width, page.Height), XStringFormats.TopLeft);
                gfx.DrawString(prixAvantTva.ToString("F2"), font, XBrushes.Black, new XRect(200, tableStartY + 2 * rowHeight, page.Width, page.Height), XStringFormats.TopLeft);

                gfx.DrawString($"TVA ({tva}%)", font, XBrushes.Black, new XRect(20, tableStartY + 3 * rowHeight, page.Width, page.Height), XStringFormats.TopLeft);
                gfx.DrawString(montantTva.ToString("F2"), font, XBrushes.Black, new XRect(200, tableStartY + 3 * rowHeight, page.Width, page.Height), XStringFormats.TopLeft);

                gfx.DrawString("Prix total", font, XBrushes.Black, new XRect(20, tableStartY + 4 * rowHeight, page.Width, page.Height), XStringFormats.TopLeft);
                gfx.DrawString(prixTotal.ToString("F2"), boldFont, XBrushes.Black, new XRect(200, tableStartY + 4 * rowHeight, page.Width, page.Height), XStringFormats.TopLeft);

                using (var stream = new MemoryStream())
                {
                    document.Save(stream);
                    return File(stream.ToArray(), "application/pdf", $"Facture_{id}.pdf");
                }
            }
        }

    }
}
