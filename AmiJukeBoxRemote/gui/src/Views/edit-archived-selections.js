import { inject, bindable, customElement, containerless } from 'aurelia-framework';
import { EventAggregator } from 'aurelia-event-aggregator';
import { MapJukeboxService } from '../services/mapjukeboxservice';
import { DialogService } from 'aurelia-dialog';
import { ConfirmDialog } from '../Components/confirmation-dialog';
import * as toastr from 'toastr';


@inject(MapJukeboxService, EventAggregator, DialogService, ConfirmDialog)

@containerless()

export class MapEditSelections {

  constructor(mapJukeboxService, eventAggregator, dialogService, confirmDialog) {
    this.mapJukeboxService = mapJukeboxService;
    this.dialogService = dialogService;
    this.ea = eventAggregator;
    this.mapJukeboxSelections = [];
    this.mapJukeboxSelection = '';
    this.confirmDialog = confirmDialog;
    this.value = 'Are you sure?';
    this.insert_Id = -1;
    this.insert_JbLetter = '';
    this.insert_JbNumberA = '';
    this.insert_JbNumberB = '';
    this.insert_JbNumeric = '';
    this.insert_A1Song = '';
    this.insert_A2Song = '';
    this.insert_B1Song = '';
    this.insert_B2Song = '';
    this.insert_Artist1 = '';
    this.insert_Artist2 = '';
    this.insert_ImageStripTemplate = 'jukecard-lightgreen-300x100.png';
    this.insert_MusicCategory = '';
    this.Insert_Archived = 0;
    this.canSave = false;
  }

  activate()
  {
    // return this.mapIdpsActivitiesService.getAllRmActivities().then(data => {
    //   this.rmActivities = data;
    //   this.mapIdpsActivitiesService.getAllIdpsActivities().then(data => {
    //     this.idpsActivities = data;
    //   })
    // });
  }

  attached() {
    return this.loadData();
  }

  loadData() {
    return this.mapJukeboxService.getAllArchivedJukeboxSelections().then(data => {
      this.mapJukeboxSelections = data;
      });
  }

  reinstateSelection(request) {
    console.log(request);
    return this.mapJukeboxService.reinstateSelection(this.insert_Id,this.insert_JbLetter,this.insert_JbNumberA);
  }

  insertSelection(request) {
    console.log(request);
    if (this.insert_JbLetter=='' || this.insert_JbNumberA=='' || this.insert_JbNumberB==''|| this.insert_JbNumeric=='' || this.insert_A1Song=='' || this.insert_B1Song=='' || this.insert_Artist1=='') return;
    if(this.insert_ImageStripTemplate=='') { this.insert_ImageStripTemplate == 'jukecard-lightgreen-300x100.png'}
    this.mapJukeboxService.insertSelection(this.insert_JbLetter,
                                                  this.insert_JbNumberA,
                                                  this.insert_JbNumberB,
                                                  this.insert_JbNumeric,
                                                  this.insert_A1Song,
                                                  this.insert_A2Song,
                                                  this.insert_B1Song,
                                                  this.insert_B2Song,
                                                  this.insert_Artist1,
                                                  this.insert_Artist2,
                                                  this.insert_ImageStripTemplate,
                                                  this.insert_MusicCategory,
                                                  this.Insert_Archived).then((request) => {
        this.loadData()
        this.insert_JbLetter ='';
        this.insert_JbNumberA ='';
        this.insert_JbNumberB ='';
        this.insert_JbNumeric = '';
        this.insert_A1Song ='';
        this.insert_A2Song ='';
        this.insert_B1Song ='';
        this.insert_B2Song ='';
        this.insert_Artist1 ='';
        this.insert_Artist2 ='';
        this.insert_MusicCategory ='';
        this.Insert_Archived
        $('#new-selection-modal').modal('hide');
     });
  }


}
