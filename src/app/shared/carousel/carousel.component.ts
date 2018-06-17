import { Component } from "@angular/core";
import * as $ from 'jquery';
import "bootstrap";

@Component({
    selector: 'bl-carousel',
    styleUrls: ['./carousel.component.css'],
    templateUrl: './carousel.component.html'
})
export class CarouselComponent{
    ngOnInit() {
        $('.carousel').carousel({
          interval: 5000
        })
    }
}