import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';

@Injectable()
export class CustomerService{

    constructor(private http: HttpClient){}

    getCustomer(customerNumber: number): Customer{

        let wantedCustomer = null;
        this.http.get<Customer>("http://localhost:57895/api/Customer/" + customerNumber)
        .subscribe((data: Customer) => wantedCustomer = data)
        return wantedCustomer;
    }
}