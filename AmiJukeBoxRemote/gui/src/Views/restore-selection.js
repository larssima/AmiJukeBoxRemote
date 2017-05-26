import { inject, NewInstance } from 'aurelia-framework';
import { DialogController } from 'aurelia-dialog';


@inject(DialogController)
export class RestoreSelection {
    dialogController = null;
    jbselection = {};    

    constructor(dialogController, validationControllerFactory, title) {
        this.dialogController = dialogController;
        this.title = title;
    }

    save() {
        this.dialogController.ok({ delete: false, save: true, jbselection: this.jbselection });
    }

    cancel() {
        this.dialogController.cancel();
    }    

    activate(data) {
        this.jbselection.Id = data.jbselection.Id;
        this.jbselection.A1Song = data.jbselection.A1Song;
        this.jbselection.A2Song = data.jbselection.A2Song;
        this.jbselection.B1Song = data.jbselection.B1Song;
        this.jbselection.B2Song = data.jbselection.B2Song;
        this.jbselection.Artist1 = data.jbselection.Artist1;
        this.jbselection.Artist2 = data.jbselection.Artist2;
        this.jbselection.ImageStripName = data.jbselection.ImageStripName;
        this.jbselection.MusicCategory = data.jbselection.MusicCategory;
        this.jbselection.Archived = 0;
        this.jbselection.ImageStripTemplate = data.jbselection.ImageStripTemplate;
        this.jbselection.jbselectioncol = data.jbselection.jbselectioncol;

        this.title = data.title;
    }    
    
}
