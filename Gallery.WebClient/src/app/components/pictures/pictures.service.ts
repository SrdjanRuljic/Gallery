import { Injectable } from '@angular/core';

@Injectable()
export class PicturesService {
    allImages = [];    
    
    getImages() {    
        return this.allImages = Imagesdelatils.slice(0);    
    } 
}

const Imagesdelatils = [    
    { "id": 1, "url": "assets/images/User.jfif", "title":"Naslov 1" },
    { "id": 2, "url": "assets/images/User.jfif", "title":"Naslov 2" },
    { "id": 2, "url": "assets/images/User.jfif", "title":"Naslov 2"},
    { "id": 2, "url": "assets/images/User.jfif", "title":"Naslov 2"},
    { "id": 2, "url": "assets/images/User.jfif", "title":"Naslov 2" },
    { "id": 2, "url": "assets/images/User.jfif", "title":"Naslov 2" },
    { "id": 2, "url": "assets/images/User.jfif", "title":"Naslov 2" },
    { "id": 2, "url": "assets/images/User.jfif", "title":"Naslov 2" },
    { "id": 2, "url": "assets/images/User.jfif", "title":"Naslov 2" },
    { "id": 3, "url": "assets/images/User.jfif", "title":"Naslov 2" }   
    
]    