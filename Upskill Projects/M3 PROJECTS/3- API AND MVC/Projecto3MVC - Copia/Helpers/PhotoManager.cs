using System;
using System.IO;



namespace Projecto3MVC.Helpers
{
    public class PhotoManager
    {
        

        public static string ReturnPhoto(byte [] photo)
        {
                        
            string imgSrc = null;
            if (photo != null)
            {
                MemoryStream ms = new MemoryStream();
                ms.Write(photo, 78, photo.Length - 78); 
                string imageBase64 = Convert.ToBase64String(ms.ToArray());
                imgSrc = string.Format("data:image/gif;base64,{0}", imageBase64);
            }
            return imgSrc;
            
        }



    }
}
