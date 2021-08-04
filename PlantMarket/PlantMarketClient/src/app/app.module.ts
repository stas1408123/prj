import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { FormsModule } from '@angular/forms';


import { AppComponent } from './app.component';
import { LoginComponent } from './auth/components/login/login.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import {MatIconModule} from '@angular/material/icon';
import {MatToolbarModule} from '@angular/material/toolbar';
import { MatFormFieldModule} from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatButtonModule } from '@angular/material/button';
import { RegisterComponent } from './auth/components/register/register.component';
import { NavComponent } from './components/nav/nav.component';
import {MatMenuModule} from '@angular/material/menu';
import {PlantComponent } from './components/plant/plant.component';
import {MatCardModule} from '@angular/material/card';
import { ListOfPlantsComponent } from './components/list-of-plants/list-of-plants.component';
import { PlantService } from './services/plant.service';
import { ShopcartComponent } from './components/shopcart/shopcart.component';
import {MatDialogModule} from '@angular/material/dialog';
import { ShopCartService } from './services/shop-cart.service';
import { HomeComponent } from './components/home/home.component';
import { EditProfileComponent } from './components/edit-profile/edit-profile.component';
import { OrderComponent } from './components/order/order.component';
import {MatExpansionModule} from '@angular/material/expansion';
import {MatListModule} from '@angular/material/list';
//import { OrderService } from './services/order.service';



@NgModule({
  exports: [
    //MatFormFieldModule,
    //MatButton,
    MatSnackBarModule
  ],
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    NavComponent,
    PlantComponent,
    ListOfPlantsComponent,
    ShopcartComponent,
    HomeComponent,
    EditProfileComponent,
    OrderComponent
  ],
  imports: [
    MatFormFieldModule,
    MatButtonModule,
    MatInputModule,
    MatToolbarModule,
    MatIconModule,
    MatMenuModule,
    MatCardModule,
    MatDialogModule,
    MatExpansionModule,
    MatListModule,


    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    BrowserAnimationsModule
  ],
  providers: [
    PlantService,
    ShopCartService//,
    //OrderService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
