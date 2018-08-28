import { Component, Input, OnChanges } from "@angular/core";

@Component({
    selector: 'bl-navbar-shopcart',
    styleUrls: ['./navbar-shopcart.component.css'],
    templateUrl: './navbar-shopcart.component.html'
})
export class NavbarShopcartComponent implements OnChanges{

    @Input()
    products: Product[];

    itemsCount: number;

    ngOnChanges(): void {
        this.itemsCount = this.products.length;
    }
}