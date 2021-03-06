import {inject} from 'aurelia-framework';
import {DialogController} from 'aurelia-dialog';

@inject(DialogController)
export class SelectionInfo {
    jbselection = {};
    title = '';

  constructor(dialogController){
    this.controller = dialogController;
  }
  activate(data){
      this.jbselection = data.jbselection;
      this.title = data.title;
  }
}