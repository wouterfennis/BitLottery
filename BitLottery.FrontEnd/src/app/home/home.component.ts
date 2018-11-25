import { Component, OnInit } from "@angular/core";
import { CustomerService } from "../services/customer-service";

@Component({
    selector: 'bl-home',
    styleUrls: ['./home.component.css'],
    providers: [ CustomerService ],
    templateUrl: './home.component.html'
})
export class HomeComponent implements OnInit{

    constructor(private customerService: CustomerService){
    }

    
    ngOnInit() { 
        console.log("test");
        this.customerService.getCustomer(1000);
    }
}