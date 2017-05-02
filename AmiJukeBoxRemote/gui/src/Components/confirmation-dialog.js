import {inject} from 'aurelia-framework';
import {DialogController} from 'aurelia-dialog';


@inject(DialogController)
export class ConfirmDialog {
    constructor(controller) {
      this.controller = controller;
      this.value = "Are you sure?";
    }

    cancel() {
        this.controller.cancel();
    }

    ok() {
        this.controller.ok();
    }

    activate(model) {
        this.value = model.value;
    }    
}