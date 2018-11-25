import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';

@Injectable()
export class ShopService{

    constructor(private http: HttpClient){}

    sellBallot(saleInfo: SaleInfo): number{
        let soldBallot = null;
        this.http.put<number>("http://localhost:57895/api/Shop/Actions/Sell/", saleInfo)
        .subscribe((data: number) => soldBallot = data)
        return soldBallot;
    }
}