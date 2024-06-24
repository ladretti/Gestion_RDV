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
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;

namespace Gestion_RDV.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiagnosesController : ControllerBase
    {
        private readonly IDataRepository<Diagnosis> dataRepository;
        private readonly IDataRepository<Prescription> dataRepositoryPrescription;
        private readonly IDataRepository<Medication> dataRepositoryMedication;
        private readonly IDataRepository<User> dataRepositoryUser;
        private readonly IDataRepository<RendezVous> dataRepositoryRendezVous;
        private readonly IDataRepository<Office> dataRepositoryOffice;
        private readonly IDataRepository<Address> dataRepositoryAdresse;
        private readonly IMapper _mapper;


        public DiagnosesController(IMapper mapper, IDataRepository<Diagnosis> dataRepo, IDataRepository<Prescription> dataRepoPrescription, IDataRepository<Medication> dataRepoyMedication, IDataRepository<User> dataRepoUser, IDataRepository<RendezVous> dataRepoRendezVous, IDataRepository<Office> dataRepoOffice, IDataRepository<Address> dataRepoAdresse)
        {
            dataRepository = dataRepo;
            _mapper = mapper;
            dataRepositoryPrescription = dataRepoPrescription;
            dataRepositoryMedication = dataRepoyMedication;
            dataRepositoryUser = dataRepoUser;
            dataRepositoryRendezVous = dataRepoRendezVous;
            dataRepositoryOffice = dataRepoOffice;
            dataRepositoryAdresse = dataRepoAdresse;
        }

        [HttpGet("generate-pdf/{id}")]
        public async Task<IActionResult> GeneratePrescriptionPdf(int id)
        {
            var diagnosis = await dataRepository.GetBySpecialIdAsync(id);
            await dataRepositoryPrescription.GetAllAsync();
            await dataRepositoryMedication.GetAllAsync();
            var rdv = await dataRepositoryRendezVous.GetByIdAsync(diagnosis.Value.RendezVousId);
            var ofc = await dataRepositoryOffice.GetByIdAsync(rdv.Value.OfficeId);
            await dataRepositoryUser.GetAllAsync();
            await dataRepositoryAdresse.GetByIdAsync(ofc.Value.OfficeId);
            if (diagnosis == null)
            {
                return NotFound();
            }


            using (var document = new PdfDocument())
            {
                var page = document.AddPage();
                var gfx = XGraphics.FromPdfPage(page);

                var headerFont = new XFont("Arial", 16, XFontStyle.Bold);
                var subHeaderFont = new XFont("Verdana", 12, XFontStyle.Bold);
                var bodyFont = new XFont("Times New Roman", 12, XFontStyle.Regular);

                int yPosition = 20;


                gfx.DrawImage(XImage.FromFile("Images/logo.jpg"), 20, yPosition, 100, 100);
                yPosition += 100;
                // Add patient information
                gfx.DrawString($"Patient: {diagnosis.Value.User.FirstName} {diagnosis.Value.User.LastName}", headerFont, XBrushes.Black, new XRect(20, yPosition, page.Width - 40, page.Height), XStringFormats.TopLeft);
                yPosition += 20;
                gfx.DrawString($"Date du diagnostique: {diagnosis.Value.DiagnosisDate:dd-MM-yyyy}", bodyFont, XBrushes.Black, new XRect(20, yPosition, page.Width - 40, page.Height), XStringFormats.TopLeft);
                yPosition += 20;
                gfx.DrawString($"Diagnostique: {diagnosis.Value.Description}", bodyFont, XBrushes.Black, new XRect(20, yPosition, page.Width - 40, page.Height), XStringFormats.TopLeft);
                yPosition += 20;
                gfx.DrawString($"Détails: {diagnosis.Value.DiagnosisDetails}", bodyFont, XBrushes.Black, new XRect(20, yPosition, page.Width - 40, page.Height), XStringFormats.TopLeft);
                yPosition += 20;

                // Add prescriptions
                gfx.DrawString("Prescriptions:", headerFont, XBrushes.Black, new XRect(20, yPosition, page.Width - 40, page.Height), XStringFormats.TopLeft);
                yPosition += 20;

                foreach (var prescription in diagnosis.Value.Prescriptions)
                {
                    var medication = prescription.Medication;
                    gfx.DrawString($"-{medication.Name}", subHeaderFont, XBrushes.Black, new XRect(40, yPosition, page.Width - 40, page.Height), XStringFormats.TopLeft);
                    yPosition += 20;
                    gfx.DrawString($"   Dosage: {medication.Dosage}", bodyFont, XBrushes.Black, new XRect(40, yPosition, page.Width - 40, page.Height), XStringFormats.TopLeft);
                    yPosition += 20;
                    gfx.DrawString($"   {prescription.Description}", bodyFont, XBrushes.Black, new XRect(40, yPosition, page.Width - 40, page.Height), XStringFormats.TopLeft);
                    yPosition += 20;
                }
                gfx.DrawString($"Dr : {diagnosis.Value.RendezVous.Office.User.FirstName} {diagnosis.Value.RendezVous.Office.User.LastName}", bodyFont, XBrushes.Black, new XRect(20, yPosition, page.Width - 40, page.Height), XStringFormats.TopLeft);
                yPosition += 20;
                using (var stream = new MemoryStream())
                {
                    document.Save(stream);
                    return File(stream.ToArray(), "application/pdf", $"Prescription_{id}.pdf");
                }
            }
        }
    }
}