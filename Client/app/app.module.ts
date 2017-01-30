import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { HttpModule } from '@angular/http';

import { HomeModule } from './home/home.module';
import { SharedModule } from './shared/shared.module';
import { Configuration } from './shared/configuration/app.configuration';
import { FoodModule } from './food/food.module';
import { AppComponent } from './app.component';
import { AppRoutes } from './app.routes';

@NgModule({
    imports: [
        BrowserModule,
        RouterModule.forRoot(AppRoutes),
        HttpModule,
        SharedModule,
        HomeModule,
        FoodModule
    ],

    declarations: [
        AppComponent
    ],

    providers: [
        Configuration
    ],

    bootstrap: [AppComponent]
})

export class AppModule { }
