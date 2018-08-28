import { Component, Input } from "@angular/core";

@Component({
    selector: 'bl-shopping-cart',
    styleUrls: ['./shopping-cart.component.css'],
    templateUrl: './shopping-cart.component.html'
})
export class ShoppingCartComponent{
    @Input()
    products: Product[];

    itemsCount: number;

    ngOnChanges(): void {
        this.itemsCount = this.products.length;
    }
}