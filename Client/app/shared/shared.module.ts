import { FoodDataService } from './services/food-data.service';
import { RouterModule } from '@angular/router';
import { NavigationComponent } from './components/navigation/navigation.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

@NgModule({
    imports: [
        // Modules
        CommonModule,
        RouterModule
    ],

    declarations: [
        // Components & directives
        NavigationComponent
    ],

    providers: [
        // Services
        FoodDataService
    ],

    exports: [
        NavigationComponent
    ]
})

export class SharedModule { }

