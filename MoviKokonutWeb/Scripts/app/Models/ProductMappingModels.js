function VideoDetails() {
    this.videoId = 0;
    this.videoName = "";
    this.videoSrc = "";
    this.items = [];
}

function ItemDetails(name, id, desc) {
    this.itemName = name;
    this.itemId = id;
    this.itemDescription = desc;
    this.itemLocations = [];

    this.itemDefaultLocation = function () {
        if (this.itemLocations.length > 0)
            return this.itemLocations[0];
        else{
            if(id == 1){
                return new ItemLocation(25, 45, 56, 85);
            }
            else {
                return new ItemLocation(78, 135, 176, 400);
            }
        }
    }
}

function ItemLocation(sTm, eTm, pX, pY) {
    this.startTime = sTm;
    this.endTime = eTm;
    this.posX = pX;
    this.posY = pY;
}

function GetVideoDetailsMock() {
    var video = new VideoDetails();
    video.videoId = 1;
    video.videoName = "Test Video";
    video.videoSrc = "https://www.youtube.com/watch?v=I7ZUkd44-Co";
    var itm1 = new ItemDetails("Item1", 1, "Item 1 description")
    itm1.itemLocations.push(new ItemLocation(25, 45, 56, 85))
    itm1.itemLocations.push(new ItemLocation(56, 90, 16, 85))
    itm1.itemLocations.push(new ItemLocation(345, 456, 35, 55))
    itm1.itemLocations.push(new ItemLocation(489, 525, 56, 25))
    itm1.itemLocations.push(new ItemLocation(600, 735, 67, 35))

    var itm2 = new ItemDetails("Item2", 2, "Item 2 description")
    itm2.itemLocations.push(new ItemLocation(78, 135, 76, 40))
    itm2.itemLocations.push(new ItemLocation(100, 126, 56, 85))
    itm2.itemLocations.push(new ItemLocation(157, 225, 35, 85))
    itm2.itemLocations.push(new ItemLocation(400, 554, 16, 22))
    itm2.itemLocations.push(new ItemLocation(600, 735, 67, 32))

    video.items = [itm1, itm2];

    return video;
}