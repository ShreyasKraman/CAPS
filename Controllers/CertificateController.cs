using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication3.Models;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Data;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.security;
using System.Drawing;
using Org.BouncyCastle.Pkcs;
using System.ComponentModel;
using System.Text;
using System.Diagnostics;
using WebMatrix.Data;
using GhostscriptSharp;
using GhostscriptSharp.Settings;


namespace MvcApplication3.Controllers
{
    public class CertificateController : Controller
    {
        X509Certificate2 cert;
        [AllowAnonymous]
        public ActionResult certificate()
        {
            return View();
        }
        List<int> percentile = new List<int>();
        [AllowAnonymous]
        [HttpPost]
        public ActionResult certificate(certiModel c, string command, HttpPostedFileBase file)
        {
            if (command == "Sign")
            {
                try
                {

                    if (file != null)
                    {

                        file.SaveAs(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory + "App_Data\\" + file.FileName));
                        var fileName = System.AppDomain.CurrentDomain.BaseDirectory + "App_Data\\" + file.FileName;
                        var certFile = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory + @"App_Data\certi.pfx");
                        cert = GetLocalStoreCertificate(certFile, "Beproject14#");
                        string dest = fileName;
                        string fname = c.fname;
                        string mname = c.mname;
                        string lname = c.lname;
                        string seat = c.seatNo;
                        var seats = System.AppDomain.CurrentDomain.BaseDirectory + "App_Data\\" +c.seatNo;
                        string consign = seats + "-mysigned.pdf";
                        ViewBag.Status = "failedss";
                        var db = Database.Open("sqlConnection");
                        //string sqlcommand = "insert into studentsCet_info values('"+c.seatNo+"'"+ "," + "'" + c.fname + "'" + "," + "'" + c.mname + "'" + "," + c.lname + ")";
                        string sqlcommand = "insert into studentCet_info values('"+seat+"'"+","+"'"+fname+"'"+","+"'"+mname+"'"+","+"'"+lname+"')";
                        db.Execute(sqlcommand);
                        int a = c.Maths + c.Physics + c.Chemistry;
                        ViewBag.Status = "failedss1";
                        string newCommand = "insert into studentsCet_marks values('"+seat+"'"+","+"'"+c.Maths+"'"+","+"'"+c.Physics+"'"+","+"'"+c.Chemistry  +"'"+","+a+")";
                        ViewBag.Status = newCommand;
                        db.Execute(newCommand);
                        string physics = c.Physics.ToString();
                        string chemistry = c.Chemistry.ToString();
                        string maths = c.Maths.ToString();
                        string join = "Physics="+physics+ "," +"Chemistry="+chemistry+ "," +"Maths="+maths;
                        
                        percentile.Add(a);
                        percentile.Sort();
                        string marks=null;
                        foreach(int i in percentile)
                        {
                            marks = marks + i.ToString() + ",";
                        }
                        ViewBag.Output = marks;
                        // int i = Decoder(fileName, dest, c.qrContent);
                      //  convertToImage("C:\\Users\\SHREYAS\\Documents\\Visual Studio 2013\\Projects\\MvcApplication3\\MvcApplication\\App_Data\\marksheetmysigned.pdf", "C:\\Users\\SHREYAS\\Documents\\Visual Studio 2013\\Projects\\MvcApplication3\\marksheet.png");

                        if (SignWithThisCert(cert, join, fileName, consign ) >= 1)
                        {
                            ViewBag.Status = "Done";
                        }
                        else
                            ViewBag.Status = "Could not find certificate";
                    }
                    else
                    {
                        ViewBag.Status = "failed";
                    }

                }

                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message.ToString());
                    return View(c);
                }
            }
            if (command == "SignPDF")
            {
                try
                {
                    //file.SaveAs(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory + "App_Data\\" + file.FileName));
                    //var fileName = System.AppDomain.CurrentDomain.BaseDirectory + "App_Data\\" + file.FileName;
                    //string consign = fileName + "-mysigneds.pdf";
                    //PdfReader pdfReader = new PdfReader(fileName);
                    //FileStream signedPdf = new FileStream(consign, FileMode.Create);
                    //iTextSharp.text.Document d = new iTextSharp.text.Document(pdfReader.GetPageSizeWithRotation(1));
                    //ViewBag.status = "Faileds";
                    //var writer = PdfWriter.GetInstance(d, signedPdf);
                    //ViewBag.status = "Failedsss";
                    //d.Open();
                    //var cb = writer.DirectContent;
                    //ViewBag.status = "Failedsss";
                    //cb.BeginText();
                    //ViewBag.status = "Failedssss";
                    //// cb.SetTextMatrix(100, 100);
                    //cb.SetFontAndSize(BaseFont.CreateFont(BaseFont.TIMES_ITALIC, BaseFont.CP1252, BaseFont.NOT_EMBEDDED), 12);
                    //cb.ShowTextAligned(iTextSharp.text.Element.ALIGN_CENTER, "Shreyas-Kalyanaraman", 300, 700, 0);
                    //ViewBag.status = "Failedsss2s";
                    //cb.EndText();
                    //ViewBag.status = "Failedsss12";
                    //d.Close();
                    //writer.Close();
                    //pdfReader.Close();
                    //signedPdf.Close();
                    // convertToImage("C:\\Users\\SHREYAS\\Documents\\Visual Studio 2013\\Projects\\MvcApplication3\\App_Data\\marksheets.pdf-s", "C:\\Users\\SHREYAS\\Documents\\Visual Studio 2013\\Projects\\MvcApplication3\\App_Data\\marksheet.png");
                    SignData(cert, c.data);

                }
                catch (Exception e)
                {
                    ViewBag.status = e.Message;
                }
            }
            return View();
        }
        private int SignWithThisCert(X509Certificate2 Certificate, string Contact, string SourcePdfFileName, string DestPdfFileName)
        {
            try
            {
                Org.BouncyCastle.X509.X509CertificateParser cp = new Org.BouncyCastle.X509.X509CertificateParser();
                Org.BouncyCastle.X509.X509Certificate[] chain = new Org.BouncyCastle.X509.X509Certificate[1];
                chain[0] = cp.ReadCertificate(Certificate.RawData);
                IExternalSignature externalSignature = new X509Certificate2Signature(Certificate, "SHA-1");
                PdfReader pdfReader = new PdfReader(SourcePdfFileName);

                FileStream signedPdf = new FileStream(DestPdfFileName, FileMode.Create); //the output pdf file
                
                PdfStamper pdfStamper = PdfStamper.CreateSignature(pdfReader, signedPdf, '\0');
                PdfContentByte pdfData = pdfStamper.GetOverContent(1);


                /*   
                   iTextSharp.text.Document d = new iTextSharp.text.Document();
                   PdfWriter writer =PdfWriter.GetInstance(d,signedPdf);
                   d.Open();
                   PdfContentByte cb = writer.DirectContent;
                   cb.BeginText();
                   
                  cb.SetFontAndSize(BaseFont.CreateFont(BaseFont.TIMES_ITALIC, BaseFont.CP1252, BaseFont.NOT_EMBEDDED), 12);
                   cb.ShowTextAligned(iTextSharp.text.Element.ALIGN_CENTER, "Shreyas-Kalyanaraman", 300, 700, 0);
                   cb.ShowText("Shreyas Kalyanaraman");
                   cb.EndText();
                   d.Close();    
                writer.Close();
                   */
                //Create the QR Code/2D Code-------------------------------
              //  Image qrImage = GenerateQRCode(Convert.ToBase64String(Certificate.RawData));

                Image qrImage = GenerateQRCode(Contact);
                iTextSharp.text.Image itsQrCodeImage = iTextSharp.text.Image.GetInstance(qrImage, System.Drawing.Imaging.ImageFormat.Jpeg);
                itsQrCodeImage.SetAbsolutePosition(270,50);
                pdfData.AddImage(itsQrCodeImage);

                //Create the QR Code/2D Code-------------------------------END

                PdfSignatureAppearance signatureAppearance = pdfStamper.SignatureAppearance;
                signatureAppearance.Acro6Layers = true;

                signatureAppearance.SetVisibleSignature(new iTextSharp.text.Rectangle(33,50,270,130), 1, null);
                //here set signatureAppearance at your will

                signatureAppearance.Contact = Contact;
                signatureAppearance.SignatureRenderingMode = PdfSignatureAppearance.RenderingMode.DESCRIPTION;

                //Stamp the PDF
                //MakeSignature.SignDetached(signatureAppearance, externalSignature, chain, null, null, null, 0, CryptoStandard.CMS);
                MakeSignature.SignDetached(signatureAppearance, externalSignature, chain, null, null, null, 0, CryptoStandard.CADES);
               // d.Close();
                //writer.Close();
                
                
                signedPdf.Close();


                return 1;
            }
            catch (Exception ex)
            {
                if (!System.IO.File.Exists(SourcePdfFileName))
                    ViewBag.Status = ex.Message;
                Exception ex2 = new Exception();
                ViewBag.Status = ex2.Message;
                throw ex2;
            }

        }
        private static Image GenerateQRCode(string Content)
        {
            ZXing.QrCode.QRCodeWriter qrc = new ZXing.QrCode.QRCodeWriter();
            ZXing.Common.BitMatrix bmx = qrc.encode(Content, ZXing.BarcodeFormat.QR_CODE, 100, 100);
            ZXing.BarcodeWriter bcw = new ZXing.BarcodeWriter();
            Bitmap bmp = bcw.Write(bmx);
            return (Image)bmp;
        }

        private X509Certificate2 GetLocalStoreCertificate(string pkcs12File, string password)
        {
            X509Certificate2 cert2 = new X509Certificate2(pkcs12File, password);
            return cert2;
        }
        private X509Certificate2 GetLocalStoreCertificate()
        {
            //Sign with certificate selection in the windows certificate store
            X509Store store = new X509Store(StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadOnly);
            X509Certificate2 cert = null;
            //manually chose the certificate in the store
            X509Certificate2Collection sel = X509Certificate2UI.SelectFromCollection(store.Certificates, null, null, X509SelectionFlag.SingleSelection);
            if (sel.Count > 0)
                cert = sel[0];
            else
            {
                ModelState.AddModelError("file", "No certificates found!!");
            }
            return cert;
        }



        private byte[] SignData(X509Certificate2 cert, string DataToSign)
        {
            byte[] buffer;
            byte[] signature;

            //Sign the data
            try
            {
                if (!DataToSign.Equals(""))
                {
                    RSACryptoServiceProvider privatekey = cert.PrivateKey as RSACryptoServiceProvider;
                    buffer = Encoding.Default.GetBytes(DataToSign);
                    signature = privatekey.SignData(buffer, new SHA1Managed());
                    return signature;
                }
                else
                    return null;
            }
            catch (Exception e)
            {
                ModelState.AddModelError("file", e.Message);
                return null;
            }
        }

        /*   private int Decoder(string SourcePdfFileName,string DestPdfFileName, string Contact)
           {
               try
               {
                
                   PdfReader pdfReader = new PdfReader(SourcePdfFileName);
                
                   FileStream signedPdfs = new FileStream(DestPdfFileName, FileMode.Create);//the output pdf file
                   PdfStamper pdfStamper = new PdfStamper(pdfReader, signedPdfs, '\0');
                   PdfContentByte pdfData = pdfStamper.GetOverContent(1);
                   //Create the QR Code/2D Code-------------------------------
                   Image qrImage = GenerateQRCode(Contact);
                   iTextSharp.text.Image itsQrCodeImage = iTextSharp.text.Image.GetInstance(qrImage, System.Drawing.Imaging.ImageFormat.Jpeg);
                   itsQrCodeImage.SetAbsolutePosition(270, 50);
                   pdfData.AddImage(itsQrCodeImage);
                   pdfStamper.Close();
                   return 1;
               }
               catch(Exception e)
               {
                   ViewBag.exception = e.Message;
                   return 0;
               }
           }*/
        private int convertToImage(string sourcePdfFilePath, string destinationPngFilePath)
        {
            try
            {
                // Use GhostscriptSharp to convert the pdf to a png
                GhostscriptWrapper.GenerateOutput(sourcePdfFilePath, destinationPngFilePath, new GhostscriptSettings
                {
                    Device = GhostscriptDevices.pngalpha,
                    Page = new GhostscriptPages
                    {
                        // Only make a thumbnail of the first page
                        Start = 1,
                        End = 1,
                        AllPages = false
                    },
                    Resolution = new Size
                    {
                        // Render at 72x72 dpi
                        Height = 72,
                        Width = 72
                    },
                    Size = new GhostscriptPageSize
                    {
                        // The dimentions of the incoming PDF must be
                        // specified. The example PDF is US Letter sized.
                        Native = GhostscriptPageSizes.a4
                    }
                }
                );
                return 1;
            }
            catch (Exception e)
            {
                ViewBag.exception = e.Message;
                return 0;
            }

        }
        [AllowAnonymous]
        public ActionResult convertPdfImg()
        {

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult convertPdfImg(HttpPostedFileBase file)
        {
            try
            {
                if (file != null)
                {
                    var filename = Path.GetFullPath(file.FileName);
                    var dest = Path.GetDirectoryName(file.FileName);
                    convertToImage("C:\\Users\\SHREYAS\\Documents\\Visual Studio 2013\\Projects\\MvcApplication3\\marksheet-sighs.pdf", "C:\\Users\\SHREYAS\\Documents\\Visual Studio 2013\\Projects\\MvcApplication3\\marksheet.png");
                    ViewBag.exception = "Done";
                    return View();
                }
            }
            catch (Exception e)
            {
                ViewBag.exception = e.Message.ToString();
            }
            return View();
        }

    }
}