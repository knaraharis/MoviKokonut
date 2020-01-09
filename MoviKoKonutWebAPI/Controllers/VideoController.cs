using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MoviKoKonutWebAPI.Controllers
{
    public class VideoDetails
    {
        public string videoId { get; set; }
        public string videoName { get; set; }
        public string videoSrc { get; set; }
        public List<ItemDetails> items { get; set; }
    }

    public class ItemDetails
    {
        public string itemName { get; set; }
        public string itemId { get; set; }
        public string itemDescription { get; set; }
        public List<ItemLocation> itemLocations { get; set; }
    }

    public class ItemLocation
    {
        public int posX { get; set; }
        public int posY { get; set; }
        public int startTime { get; set; }
        public int endTime { get; set; }
    }

    [RoutePrefix("api/video")]
    public class VideoController : ApiController
    {

        private List<VideoDetails> allVideos;
        public VideoController()
        {
            allVideos = new List<VideoDetails>();
            allVideos.Add(new VideoDetails { videoId = "PHtXp5g-aRE", videoName = "Krishnashtami", videoSrc = "Youtube" });
            allVideos.Add(new VideoDetails { videoId = "IPX7qBywPVs", videoName = "Akhil", videoSrc = "Youtube" });
            allVideos.Add(new VideoDetails { videoId = "A18hRD1-TFo", videoName = "Dictator", videoSrc = "Youtube" });
            allVideos.Add(new VideoDetails { videoId = "A1aNJ0fcApA", videoName = "Sher", videoSrc = "Youtube" });
            allVideos.Add(new VideoDetails { videoId = "Qs-ITgQjuGA", videoName = "Shankarabharanam", videoSrc = "Youtube" });
            allVideos.Add(new VideoDetails { videoId = "yJBQ23Fx1-U", videoName = "Bengal Tiger", videoSrc = "Youtube" });
            allVideos.Add(new VideoDetails { videoId = "cffDigH7mKM", videoName = "Dynamite", videoSrc = "Youtube" });
            allVideos.Add(new VideoDetails { videoId = "q3_T8Px8K0k", videoName = "Loafer", videoSrc = "Youtube" });
            allVideos.Add(new VideoDetails { videoId = "EJaVQds2teQ", videoName = "Sahasam swasaga sagipo", videoSrc = "Youtube" });
            allVideos.Add(new VideoDetails { videoId = "HvIoH1BcR98", videoName = "Maanzaa", videoSrc = "Youtube" });
        }
        // GET: api/Videos
        [Route("")]
        public IEnumerable<VideoDetails> GetAll()
        {
            return allVideos;
        }

        // GET: api/Videos/5
        [Route("{id}")]
        public VideoDetails Get(string id)
        {
            VideoDetails vid = null;
            try
            {
                vid = allVideos.Where(x => x.videoId == id).FirstOrDefault();
                if (vid != null)
                {
                    vid.items = new List<ItemDetails>();
                    ItemDetails i1 = new ItemDetails() { itemId = "1", itemDescription = "Item 1 description", itemName = "Item No 1", itemLocations = new List<ItemLocation>() };
                    i1.itemLocations.Add(new ItemLocation() { startTime = 25, endTime = 30, posX = 56, posY = 85 });
                    i1.itemLocations.Add(new ItemLocation() { startTime = 5, endTime = 12, posX = 35, posY = 55 });
                    i1.itemLocations.Add(new ItemLocation() { startTime = 55, endTime = 60, posX = 67, posY = 35 });

                    ItemDetails i2 = new ItemDetails() { itemId = "2", itemDescription = "Item 2 description", itemName = "Item No 2", itemLocations = new List<ItemLocation>() };
                    i2.itemLocations.Add(new ItemLocation() { startTime = 14, endTime = 22, posX = 76, posY = 40 });
                    i2.itemLocations.Add(new ItemLocation() { startTime = 7, endTime = 30, posX = 16, posY = 22 });
                    i2.itemLocations.Add(new ItemLocation() { startTime = 40, endTime = 48, posX = 38, posY = 75 });

                    vid.items.Add(i1);
                    vid.items.Add(i2);
                }
            }
            catch(Exception e)
            {

            }
            return vid;
        }

        [Route("popular")]
        public IEnumerable<VideoDetails> GetByPopularity()
        {
            List<VideoDetails> videos = new List<VideoDetails>();
            try
            {
                int count = 0;
                foreach(VideoDetails vid in allVideos)
                {
                    if (count <= 3)
                    {
                        vid.items = new List<ItemDetails>();
                        ItemDetails i1 = new ItemDetails() { itemId = "1", itemDescription = "Item 1 description", itemName = "Item No 1", itemLocations = new List<ItemLocation>() };
                        i1.itemLocations.Add(new ItemLocation() { startTime = 25, endTime = 30, posX = 56, posY = 85 });
                        i1.itemLocations.Add(new ItemLocation() { startTime = 5, endTime = 12, posX = 35, posY = 55 });
                        i1.itemLocations.Add(new ItemLocation() { startTime = 55, endTime = 60, posX = 67, posY = 35 });

                        ItemDetails i2 = new ItemDetails() { itemId = "2", itemDescription = "Item 2 description", itemName = "Item No 2", itemLocations = new List<ItemLocation>() };
                        i2.itemLocations.Add(new ItemLocation() { startTime = 14, endTime = 22, posX = 76, posY = 40 });
                        i2.itemLocations.Add(new ItemLocation() { startTime = 7, endTime = 30, posX = 16, posY = 22 });
                        i2.itemLocations.Add(new ItemLocation() { startTime = 40, endTime = 48, posX = 38, posY = 75 });

                        vid.items.Add(i1);
                        vid.items.Add(i2);

                        videos.Add(vid);
                        count++;
                    }
                }
                
            }
            catch (Exception e)
            {

            }
            return videos;
        }

        // POST: api/Videos
        [Route("")]
        public void Post([FromBody]VideoDetails value)
        {

        }

        // PUT: api/Videos/5
        [Route("{id}")]
        public void Put(string id, [FromBody]VideoDetails value)
        {
        }

        // DELETE: api/Videos/5
        [Route("{id}")]
        public void Delete(string id)
        {
        }
    }
}
