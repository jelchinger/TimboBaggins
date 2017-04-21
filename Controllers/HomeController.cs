// Code goes here

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using ImageMagick;
using tester.Models;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using tester.Data;

namespace tester.Controllers
{
    public class HomeController : Controller
    {
        private ProjectContext db;

        public HomeController(ProjectContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
        public IActionResult notIndex()
        {

            Image image = new Image();
            image = processExif(@"");

            return View(image);

        }

        [HttpPost]
        [Route("api/uploadFile")]
        public async Task<int> Upload(IFormFile file)
        {
            var uploads = @"C:\tester\testUser";
            Image image = new Image();
            // this is where upload of photos begin
            if (file.Length > 0)
            {
                string myString = file.FileName.ToString();
                string[] subStrings = myString.Split('/');
                var filePath = Path.Combine(uploads, subStrings[subStrings.Length - 1]);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                // this is where upload ends, and pulling meta data begins
                image = processExif(filePath);
                if (image == null)
                {

                }
                else
                {
                    var addImage = new Image
                    {
                        filePath = image.filePath,
                        GPSLatitudeRef = image.GPSLatitudeRef,
                        GPSLatitude = image.GPSLatitude,
                        LatitudeDegree = image.LatitudeDegree,
                        LatitudeSecond = image.LatitudeSecond,
                        GPSLongitudeRef = image.GPSLongitudeRef,
                        GPSLongitude = image.GPSLongitude,
                        LongitudeDegree = image.LongitudeDegree,
                        LongitudeMinute = image.LongitudeMinute,
                        LongitudeSecond = image.LongitudeSecond,
                        photoDate = image.photoDate,
                        isActive = true,
                        dateCreated = DateTime.Now
                    };

                    db.Image.Add(addImage);
                    await db.SaveChangesAsync();

                }
            }

            return 1;

        }
        //[HttpPost]
        //[Route("api/uploadFile")]
        //public async Task<int> Upload(IList<IFormFile> files)
        //{
        //    var uploads = @"C:\tester\testUser\";
        //    foreach (var file in files)
        //    {
        //        if (file.Length > 0)
        //        {
        //            var filePath = Path.Combine(uploads, file.FileName);
        //            using (var fileStream = new FileStream(filePath, FileMode.Create))
        //            {
        //                await file.CopyToAsync(fileStream);
        //            }
        //        }
        //    }
        //    return 1;
        //}
        public ArrayList getFilesNames()
        {

            ArrayList paths = new ArrayList();

            string source = @"C:\Photo";

            try
            {
                var jpgFiles = Directory.EnumerateFiles(source, "*.jpg");

                foreach (string file in jpgFiles)
                {
                    paths.Add(file);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return paths;
        }

        public Image processExif(string path)
        {

            Image image = new Image();
            image.filePath = path;

            using (MagickImage exifImage = new MagickImage(path))
            {
                // Retrieve the exif information

                ExifProfile profile = exifImage.GetExifProfile();

                // Check if image contains an exif profile
                if (profile == null)
                    image.hasExif = false;
                else
                {
                    image.hasExif = true;
                    foreach (ExifValue value in profile.Values)
                    {
                        Console.WriteLine(value.Tag.ToString() + ": " + value.ToString());
                        setAllValues(value, image);
                    }
                }
            }

            return image;
        }

        public void setAllValues(ExifValue value, Image image)
        {

            string hold = value.ToString();

            if (value.Tag.ToString().Equals("GPSLatitudeRef"))
            {

                image.GPSLatitudeRef = hold[0];

            }
            else if (value.Tag.ToString().Equals("GPSLatitude"))
            {

                image.GPSLatitude = hold;
                image.LatitudeDegree = parseDegree(hold);
                image.LatitudeMinute = parseMinute(hold);
                image.LatitudeSecond = parseSecond(hold);

            }
            else if (value.Tag.ToString().Equals("GPSLongitudeRef"))
            {

                image.GPSLongitudeRef = hold[0];

            }
            else if (value.Tag.ToString().Equals("GPSLongitude"))
            {

                image.GPSLongitude = hold;
                image.LongitudeDegree = parseDegree(hold);
                image.LongitudeMinute = parseMinute(hold);
                image.LongitudeSecond = parseSecond(hold);

            }
            else if (value.Tag.ToString().Equals("DateTime"))
            {

                image.photoDate = convertDateTime(hold);

            }
        }

        public int parseDegree(string value)
        {

            Boolean run = true;
            int a = 0;
            int b = 0;

            while (run)
            {
                if (value[b].ToString() == " ")
                {
                    string sub = value.Substring(a, b);
                    a = Convert.ToInt32(sub);
                    run = false;
                }
                b = b + 1;
            }

            return a;
        }

        public int parseMinute(string value)
        {

            Boolean run = true;
            Boolean next = false;
            int a = 0;
            int b = 0;

            while (run)
            {
                if (next)
                {
                    if (value[b].ToString() == " ")
                    {
                        string sub = value.Substring(a, b - a);
                        a = Convert.ToInt32(sub);
                        run = false;
                    }
                }
                else
                {
                    if (value[b].ToString() == " ")
                    {
                        a = b + 1;
                        next = true;
                    }
                }
                b = b + 1;
            }

            return a;
        }

        public double parseSecond(string value)
        {

            Boolean run = true;
            Boolean next = false;
            double num = 0;
            double den = 0;
            int a = 0;
            int b = 0;
            int length = value.Length;
            string sub = "";

            while (run)
            {
                if (next)
                {
                    if (sub.Length == 2 || sub.Length == 3)
                    {
                        num = Convert.ToDouble(sub);
                        run = false;
                    }
                    else if (sub[b].ToString() == "/")
                    {
                        num = Convert.ToDouble(sub.Substring(a, b));
                        den = Convert.ToDouble(sub.Substring(b + 1, sub.Length - (b + 1)));
                        num = num / den;
                        run = false;
                    }
                }
                else
                {
                    if (value[b].ToString() == " ")
                    {
                        a++;
                        if (a == 2)
                        {
                            next = true;
                            sub = value.Substring(b, length - b);
                            b = 0;
                            a = 0;
                        }
                    }
                }
                b++;
            }

            return num;
        }

        public DateTime convertDateTime(string value)
        {

            int hour = Convert.ToInt32(value.Substring(11, 2));
            string amPm = "AM";

            if (hour > 12)
            {
                if (hour == 24)
                {
                    amPm = "AM";
                }
                else
                {
                    amPm = "PM";
                }
                hour = hour - 12;
            }
            else
            {
                if (hour == 12)
                {
                    amPm = "PM";
                }
            }

            string hold = value.Substring(5, 2) + "/" + value.Substring(8, 2) + "/" + value.Substring(0, 4) + " " + hour.ToString() + ":" + value.Substring(14, 2) + ":" + value.Substring(17, 2) + " " + amPm;

            return Convert.ToDateTime(hold);
        }

    }
}