import {inject} from 'aurelia-framework';
import {DialogController} from 'aurelia-dialog';

@inject(DialogController)
export class ChooseSideDialog2 {
    constructor(controller) {
      this.controller = controller;
      this.sideA = "Choose sideA!"
      this.sideB = "Choose sideB!";
      this.que = true;
    }

    front() {
        this.controller.ok();
    }

    back() {
        this.controller.cancel();
    }

    activate(model) {
        this.sideA = model.sideA;
        this.sideB = model.sideB;
        this.que = model.que;
    }    
}