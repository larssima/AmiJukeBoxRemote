import {inject} from 'aurelia-framework';
import {DialogController} from 'aurelia-dialog';


@inject(DialogController)
export class ChooseSideDialog {
    constructor(controller) {
      this.controller = controller;
      this.value = "Choose side!";
    }

    front() {
        this.controller.ok();
    }

    back() {
        this.controller.cancel();
    }

    activate(model) {
        this.value = model.value;
    }    
}