import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';


import { AppComponent } from './app.component';
import { BallotCounterComponent } from './ballot-counter/ballot-counter-component';
import { BallotsComponent } from './ballots/ballots.component';
import { HomeComponent } from './home/home.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { ShoppingCartComponent } from './shopping-cart/shopping-cart.component';
import { NavbarComponent } from './shared/navbar/navbar.component';
import { NavbarShopcartComponent } from './shared/navbar/navbar-shopcart/navbar-shopcart.component';
import { NavLoginComponent } from './shared/navbar/navbar-login/navbar-login.component';
import { CarouselComponent } from './shared/carousel/carousel.component';
import { FooterComponent } from './shared/footer/footer.component';

const appRoutes: Routes = [
  { 
    path: 'home', 
    component: HomeComponent 
  },
 // { path: 'about-us',      component: AboutUsComponent },
  {
    path: 'shopping-cart',
    component: ShoppingCartComponent,
  },
  { path: '',
    redirectTo: '/home',
    pathMatch: 'full'
  },
  { path: '**', component: PageNotFoundComponent }
];


@NgModule({
  declarations: [
    AppComponent,
    BallotCounterComponent,
    BallotsComponent,    
    HomeComponent,
    PageNotFoundComponent,
    ShoppingCartComponent,
    NavbarComponent,
    NavbarShopcartComponent,
    NavLoginComponent,
    CarouselComponent,
    FooterComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    RouterModule.forRoot(
      appRoutes,
      { enableTracing: true } // <-- debugging purposes only
    )
   ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
