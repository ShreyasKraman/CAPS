using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication3.Models;
using WebMatrix.Data;
using WebMatrix.WebData;
using System.Collections;
using System.IO;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Drawing.Imaging;
namespace MvcApplication3.Controllers
{
    public class ApplicationFormController : Controller
    {


        public SelectList dropdown(string[] sel)
        {
            List<SelectListItem> state = new List<SelectListItem>();
            state.Add(new SelectListItem { Text = "Maharashtra", Value = "Maharashtra" });
            state.Add(new SelectListItem { Text = "Gujarat", Value = "Gujarat" });
            return new SelectList(state, "Text", "Value", sel);

        }
        [AllowAnonymous]
        public ActionResult ApplicationForm(application_form model)
        {
            var dbs = Database.Open("sqlConnection");
            ViewBag.StateName = dropdown(null);
            string newCommand = "SELECT count(*) FROM application_form WHERE userid='" + User.Identity.Name + "'";
            ViewBag.status = newCommand;
            int val = dbs.QueryValue(newCommand);
            if(val==1)
            {
                ModelState.AddModelError("","You have already submitted the Application Form, If You fill again previous details will be replaced.");
                return View(model);
            }

            


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ApplicationForm(application_form model, FormCollection form)
        {
            string value = form["States"];
            ViewBag.StateName = dropdown(value.Split(','));
            try
            {
                
                    var dbs = Database.Open("sqlConnection");
                    string[] content;
                    content = new string[19];
                    content[0] = model.fname;
                    content[1] = model.mname;
                    content[2] = model.lname;
                    content[3] = model.fatname;
                    content[4] = model.Maaname;
                    content[5] = form["Gender"];
                    content[6] = model.calender;
                    content[7] = form["Religion"];
                    content[8] = form["MT"];
                    content[9] = model.add1;
                    content[10] = model.add2;
                    content[11] = model.add3;
                    content[12] = value;
                    content[13] = form["District"];
                    content[14] = form["Taluka"];
                    content[15] = model.pinCode;
                    content[16] = model.mob;
                    content[17] = User.Identity.Name;
                content[18]=form["Category"];
                    string join = null;
                    for (int i = 0; i < content.Length-1; i++)
                    {
                        join =join+ "'" + content[i] + "'" + ",";
                    }
                   // ViewBag.status = join;
                    string newCommand = "SELECT count(*) FROM application_form WHERE userid='" +content[17]+"'";
                    ViewBag.status = newCommand;
                    int val = dbs.QueryValue(newCommand);
                if(val==1)
                {
                    var newmodel = new application_form
                    {
                        fname = dbs.QueryValue("SELECT fname FROM application_form WHERE userid='" + content[17] + "'"),
                        mname = dbs.QueryValue("SELECT mname FROM application_form WHERE userid='" + content[17] + "'"),
                        lname = dbs.QueryValue("SELECT lname FROM application_form WHERE userid='" + content[17] + "'"),
                fatname = dbs.QueryValue("SELECT faname FROM application_form WHERE userid='" + content[17] + "'"),
                Maaname = dbs.QueryValue("SELECT moname FROM application_form WHERE userid='" + content[17] + "'"),
                gender = dbs.QueryValue("SELECT gender FROM application_form WHERE userid='" + content[17] + "'"),
                calender = dbs.QueryValue("SELECT dob FROM application_form WHERE userid='" + content[17] + "'"),
                religion = dbs.QueryValue("SELECT religion FROM application_form WHERE userid='" + content[17] + "'"),
                mt = dbs.QueryValue("SELECT mtounge FROM application_form WHERE userid='" + content[17] + "'"),
                add1 = dbs.QueryValue("SELECT add1 FROM application_form WHERE userid='" + content[17] + "'"),
                add2 = dbs.QueryValue("SELECT add2 FROM application_form WHERE userid='" + content[17] + "'"),
                add3 = dbs.QueryValue("SELECT add3 FROM application_form WHERE userid='" + content[17] + "'"),
                state = dbs.QueryValue("SELECT states FROM application_form WHERE userid='" + content[17] + "'"),
                // = dbs.QueryValue("SELECT district FROM application_form WHERE userid='" + content[17] + "'"),
                //form["Taluka"] = dbs.QueryValue("SELECT taluka FROM application_form WHERE userid='" + content[17] + "'"),
                pinCode = dbs.QueryValue("SELECT pincode FROM application_form WHERE userid='" + content[17] + "'"),
                mob = dbs.QueryValue("SELECT mobileNo FROM application_form WHERE userid='" + content[17] + "'"),
  
                    };
                return View(newmodel);
                }
                else 
                { 
                    string command = "insert into application_form values("+ join + "'" + content[18] + "')";//+ "'" + content[16] + "')";
                    dbs.Execute(command);
                    return RedirectToAction("preferenceCollege");
                }
                //return RedirectToAction("verifyCertificate");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message.ToString());
                return View(model);
            }
        }

        public JsonResult DistrictList(string Id)
        {
            var district = from s in District.GetDistrict()
                           where s.StateName == Id
                           select s;

            return Json(new SelectList(district.ToArray(), "StateName", "DistrictName"), JsonRequestBehavior.AllowGet);
        }

        public JsonResult TalukaList(string Id)
        {
            var taluka = from s in Taluka.GetTaluka()
                         where s.DistrictName == Id
                         select s;

            return Json(new SelectList(taluka.ToArray(), "DistrictName", "TalukaName"), JsonRequestBehavior.AllowGet);
        }

        public MultiSelectList prefrence(string[] selectedValue)
        {

            // preferenceModel model = new preferenceModel();
            //model.colleges = new string[] { "1","2"};
            List<SelectListItem> clgnames = new List<SelectListItem>();
            clgnames.Add(new SelectListItem { Text = "SIES GST", Value = "SIES GST" });
            clgnames.Add(new SelectListItem { Text = "RAIT", Value = "RAIT" });
            clgnames.Add(new SelectListItem { Text = "A.C Patil", Value = "A.C Patil" });
            clgnames.Add(new SelectListItem { Text = "Bharti Vidyapeet", Value = "Bharti Vidyapeet" });
            clgnames.Add(new SelectListItem { Text = "Indira Gandhi", Value = "Indira Ghandi" });
            clgnames.Add(new SelectListItem { Text = "KJSCE", Value = "KJSCE" });
            //model.collegeName = clgnames;
            return new MultiSelectList(clgnames, "Value", "Text", selectedValue);
        }

        public ActionResult preferenceCollege(string[] Name)
        {
            ViewBag.collegeList = prefrence(null);
            return View();
        }
        List<string> list = new List<string>();
        [HttpPost]
        public ActionResult preferenceCollege(FormCollection form, string command, preferenceCollege model)
        {
            //list.Add(form["Colleges"]);
            /*  if(command=="Add")
              {
                  string selectedValues = form["Colleges"];
                  ViewBag.collegeList = prefrence(selectedValues.Split(','));
                  list.Add(form["Colleges"]);
                  return View();

              }*/
            ///  else if(command == "Save")
            //{ 
            ViewBag.YouSelected = form["Colleges"];

            string selectedValues = form["Colleges"];

            ViewBag.collegeList = prefrence(selectedValues.Split(','));

            //string abc = form["Colleges"];
            //string[] select = selectedValues.Split(',');
            //int ret = generateCsvFile(list, list.Count);

            //if (ret == 1)
            //{
                list.Clear();
                return RedirectToAction("payment", "Payment");
            //}
            //else
            //{
            //    return RedirectToAction("About", "Home");
            //}
            //return RedirectToAction("Index", "Home");
            //}

            /*  else
              {
                  string selectedValues = form["Colleges"];
                  ViewBag.collegeList = prefrence(selectedValues.Split(','));
                  list.Remove(form["Colleges"]);
                  return View();
              }
             */
        }

        [HttpPost]
        public JsonResult selectedCollegeList(string[] array)
        {
            list = array.ToList();
            //generateCsvFile(list, list.Count);
            string a = null;
            foreach(string i in list)
            {
                a += i + ",";
            }
            int b = generateCsvFile(list, list.Count);
            if (b==1)
            {
                string result = "saved";
                return Json(result);
            }
            else
            {
                return Json(null);
            }

        }


        private int generateCsvFile(List<string> list, int count)
        {
            try
            {
                var filepath = System.AppDomain.CurrentDomain.BaseDirectory + "App_Data\\" + "preference.csv";
                if (!System.IO.File.Exists(filepath))
                {
                    System.IO.File.Create(filepath).Close();
                }
                string delimit = ",";
                StringBuilder sb = new StringBuilder();
                string[][] outs;
                string join = null;
                string[] array = list.ToArray();
                for (int i = 0; i < array.Length; i++)
                {
                    join =join+ array[i] + ",";
                }
                outs = new string[][] { new string[] { '"' + join + '"' } };

                //for (int i = 0; i < array.Length;i++ )


                int len = outs.Length;
                //Array.Copy(array,outs[0],array.Length);
                for (int index = 0; index < len; index++)
                {
                    sb.AppendLine(string.Join(delimit, outs[index]));
                }


                System.IO.File.AppendAllText(filepath, sb.ToString());
                return 1;
            }
            catch (Exception e)
            {
                ViewBag.me = e.Message.ToString();
                return 0;
            }
            //string a = User.Identity.Name;
        }
        public JsonResult SelectedCollege(string Id)
        {
            var district = from s in District.GetDistrict()
                           where s.StateName == Id
                           select s;

            return Json(new SelectList(district.ToArray(), "StateName", "DistrictName"), JsonRequestBehavior.AllowGet);
        }

       
        [AllowAnonymous]
        public ActionResult verifyCertificate()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult verifyCertificate(verifyCertificate model, HttpPostedFileBase file)
        {
            try
            {
                if (file != null)
                {
                    file.SaveAs(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory + "App_Data\\" + file.FileName));
                    var filename = System.AppDomain.CurrentDomain.BaseDirectory + "App_Data\\" + file.FileName;
                   
                   string a = extractImagefromPdf(filename);
                   ViewBag.Status = a;
                    return View();
                }
                else
                {
                    ViewBag.status = "file not there";
                }
            }
            catch(Exception e)
            {
                ModelState.AddModelError("file", e.Message);
            }

            return View();

        }

        public static string extractImagefromPdf(string pdfSource)
        {
            //var pdfsource = System.AppDomain.CurrentDomain.BaseDirectory + "App_Data\\marksheet.pdf-mysigned";
            PdfReader pdf = new PdfReader(pdfSource);
            RandomAccessFileOrArray raf = new iTextSharp.text.pdf.RandomAccessFileOrArray(pdfSource);
            try
            {
                PdfDictionary pd = pdf.GetPageN(1);
                PdfDictionary res = (PdfDictionary)PdfReader.GetPdfObject(pd.Get(PdfName.RESOURCES));
                PdfDictionary xobj = (PdfDictionary)PdfReader.GetPdfObject(res.Get(PdfName.XOBJECT));
                if (xobj != null)
                {
                    foreach (PdfName name in xobj.Keys)
                    {
                        PdfObject obj = xobj.Get(name);
                        if (obj.IsIndirect())
                        {
                            PdfDictionary tg = (PdfDictionary)PdfReader.GetPdfObject(obj);
                            PdfName type = (PdfName)PdfReader.GetPdfObject(tg.Get(PdfName.SUBTYPE));
                            if (PdfName.IMAGE.Equals(type))
                            {
                                int XrefIndex = Convert.ToInt32(((PRIndirectReference)obj).Number.ToString(System.Globalization.CultureInfo.InvariantCulture));
                                PdfObject pdfObj = pdf.GetPdfObject(XrefIndex);
                                PdfStream pdfStrem = (PdfStream)pdfObj;
                                byte[] bytes = PdfReader.GetStreamBytesRaw((PRStream)pdfStrem);
                                if ((bytes != null))
                                {
                                    using (System.IO.MemoryStream memStream = new System.IO.MemoryStream(bytes))
                                    {
                                        memStream.Position = 0;
                                        System.Drawing.Image img = System.Drawing.Image.FromStream(memStream);
                                        var outputPath = System.AppDomain.CurrentDomain.BaseDirectory + "App_Data\\";
                                        if (!Directory.Exists(outputPath))
                                            Directory.CreateDirectory(outputPath);
                                        string path = Path.Combine(outputPath, String.Format(@"{0}.bmp", 1));
                                        System.Drawing.Imaging.EncoderParameters parms = new System.Drawing.Imaging.EncoderParameters(1);
                                        parms.Param[0] = new System.Drawing.Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.Compression, 0);
                                        System.Drawing.Imaging.ImageCodecInfo jpegEncoder = GetImageEncoder("BMP");
                                        img.Save(path, jpegEncoder, parms);

                                    }
                                }
                            }
                        }
                    }
                }
                return "completed";
            }
            catch (Exception e)
            {
                return e.Message.ToString();
            }
            finally
            {
                pdf.Close();
            }
        }
             public static System.Drawing.Imaging.ImageCodecInfo GetImageEncoder(string imageType)
             {
                 imageType = imageType.ToUpperInvariant();
                 foreach (ImageCodecInfo info in ImageCodecInfo.GetImageEncoders())
                 {
                     if (info.FormatDescription == imageType)
                     {
                         return info;
                     }
                 }
                 return null;
             }
        


    }
}