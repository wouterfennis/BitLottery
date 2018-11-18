import { Component, Input, OnChanges } from "@angular/core";

@Component({
    selector: 'bl-ballot-counter',
    styleUrls: ['./ballot-counter-component.css'],
    templateUrl: './ballot-counter-component.html'
})
export class BallotCounterComponent implements OnChanges{

    @Input()
    startCount: number

    counter: Counter

    constructor(){
    }

    ngOnInit() { 
        this.infiniteRaiseCounter();
    }

    ngOnChanges(): void {
        let newCounter: Counter = this.extractDigitsFromNumber(this.startCount);
        this.counter = newCounter;
    }

    private extractDigitsFromNumber(number : number) : Counter{
        let startOnes = Math.floor(number % 10);
        let startTens = Math.floor((number / 10) % 10);
        let startHundreds = Math.floor((number / 100) %10);        
        let startThousands = Math.floor(number / 1000);

        let counter : Counter = {
            ones: startOnes,
            tens: startTens,
            hundreds: startHundreds,
            thousands: startThousands
        }
        return counter;
    }
    
    private infiniteRaiseCounter(){
        var self = this;
        var randomSeconds = self.generateRandomNumber(500, 2000);
        setTimeout(function () {
            self.raiseCounter();
            self.infiniteRaiseCounter();
        }, randomSeconds);
    }

    private raiseCounter(){
        this.counter.ones++;
        if(this.counter.ones > 9){
            this.counter.ones = 0
            this.counter.tens++;
        }
        
        if(this.counter.tens > 9){
            this.counter.tens = 0
            this.counter.hundreds++;
        }
        
        if(this.counter.hundreds > 9){
            this.counter.hundreds = 0
            this.counter.thousands++;
        }
    }

    private generateRandomNumber(min: number, max: number) : number{
        return Math.floor((Math.random() * max) + min);
    }
}