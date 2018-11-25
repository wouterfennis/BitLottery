import { Component, EventEmitter } from "@angular/core";

@Component({
    selector: 'bl-products',
    styleUrls: ['./products.component.css'],
    templateUrl: './products.component.html'
})
export class ProductsComponent{

    public ballotPicked: EventEmitter<Product>;

    products: Product[] = [
        {
            productNumber: 1001,
            title: "1 full ballot",
            image: "http://via.placeholder.com/100x100",
            description: "Winning 1/5 of the price for 1/5 of the money.",
            price: 10
        },
        {
            productNumber: 1002,
            title: "2 full ballot",
            image: "http://via.placeholder.com/100x100",
            description: "Winning 1/5 of the price for 1/5 of the money.",
            price: 10
        },
        {
            productNumber: 1003,
            title: "3 full ballot",
            image: "http://via.placeholder.com/100x100",
            description: "Winning 1/5 of the price for 1/5 of the money.",
            price: 10
        },
    ]

    public PickProduct(product: Product){
        this.ballotPicked.emit(product);
    }
}