import { Component } from "@angular/core";

@Component({
    selector: 'bl-ballots',
    styleUrls: ['./ballots.component.css'],
    templateUrl: './ballots.component.html'
})
export class BallotsComponent{

    ballots: Ballot[] = [
        {
            image: "http://via.placeholder.com/100x100",
            title: "1 full ballot",
            description: "Winning 1/5 of the price for 1/5 of the money."
        },
        {
            image: "http://via.placeholder.com/100x100",
            title: "1/2 ballot",
            description: "Winning 1/5 of the price for 1/5 of the money."
        },
        {
            image: "http://via.placeholder.com/100x100",
            title: "1/5 ballot",
            description: "Winning 1/5 of the price for 1/5 of the money."
        }
    ]
}