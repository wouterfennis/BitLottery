import { Component } from "@angular/core";
import { NavLoginComponent } from "./navbar-login/navbar-login.component";

@Component({
  selector: 'bl-navbar',
  styleUrls: ['./navbar.component.css'],
  templateUrl: './navbar.component.html'
})
export class NavbarComponent {

  product: Product = {
    title: "ddf",
    image: "df",
    description: "",
    price: 2,
    amount: 2
  }

  products = [this.product, this.product];
}
