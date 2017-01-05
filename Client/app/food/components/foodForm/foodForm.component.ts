import { FoodItem } from './../../../shared/models/foodItem';
import { Component, Input, OnChanges, Output, EventEmitter, SimpleChanges } from '@angular/core';
import { NgForm } from '@angular/forms';

@Component({
    moduleId: module.id,
    selector: 'foodform',
    templateUrl: './foodForm.component.html'
})

export class FoodFormComponent implements OnChanges {
    @Input() foodItem: FoodItem;

    @Output() foodUpdated = new EventEmitter<FoodItem>();
    @Output() foodAdded = new EventEmitter<FoodItem>();
    @Output() isCancelled = new EventEmitter<FoodItem>();

    public isInUpdateMode: boolean = false;
    public currentFood: FoodItem;

    constructor() { }

    ngOnChanges(changes: SimpleChanges) {
        console.log('in ngOnChanges');
        this.currentFood = Object.assign(new FoodItem(), changes['foodItem'].currentValue);
        this.isInUpdateMode = !!this.currentFood.id;
    }

    public AddOrUpdateFood = (): void => {
        this.isInUpdateMode = false;
        if (this.foodItem.id) {
            console.log('update');
            this.foodUpdated.emit(this.currentFood);
        } else {
            console.log('add');
            this.foodAdded.emit(this.currentFood);
        }
    }

    public CancelUpdate(form: NgForm): void {
        this.isCancelled.emit(new FoodItem());
    }
}
