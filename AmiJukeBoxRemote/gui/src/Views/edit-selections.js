import { inject, bindable, customElement, containerless, observable } from 'aurelia-framework';
import { EventAggregator } from 'aurelia-event-aggregator';
import { MapJukeboxService } from '../services/mapjukeboxservice';
import { DialogService } from 'aurelia-dialog';
import { ConfirmDialog } from '../Components/confirmation-dialog';
import * as toastr from 'toastr';


@inject(MapJukeboxService, EventAggregator, DialogService, ConfirmDialog, observable)

@containerless()

export class MapEditSelections {
@observable insert_JbNumberA;

  constructor(mapJukeboxService, eventAggregator, dialogService, confirmDialog, observable) {
    this.mapJukeboxService = mapJukeboxService;
    this.dialogService = dialogService;
    this.ea = eventAggregator;
    this.mapJukeboxSelections = [];
    this.mapJukeboxSelection = '';
    this.confirmDialog = confirmDialog;
    this.value = 'Are you sure?';
    this.insert_JbLetter = '';
    // this.insert_JbNumberA = '';
    this.observable = observable;
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

  isOdd(num) { return num % 2;}

  insert_JbNumberAChanged(newValue, oldValue)
  {
    console.log(newValue,this.insert_JbLetter);
    var numba = parseInt(newValue);
    if(newValue<1 || newValue>19) return
    var recordnr = -1
    if(this.insert_JbLetter=='A') recordnr=0
    if(this.insert_JbLetter=='B') recordnr=10
    if(this.insert_JbLetter=='C') recordnr=20
    if(this.insert_JbLetter=='D') recordnr=30
    if(this.insert_JbLetter=='E') recordnr=40
    if(this.insert_JbLetter=='F') recordnr=50
    if(this.insert_JbLetter=='G') recordnr=60
    if(this.insert_JbLetter=='H') recordnr=70
    if(this.insert_JbLetter=='J') recordnr=80
    if(this.insert_JbLetter=='K') recordnr=90
    if(this.insert_JbLetter==-1 || !this.isOdd(numba)) return
    var num = (numba + 1) / 2
    recordnr = recordnr + num    
    this.insert_JbNumberB = numba + 1;
    this.insert_JbNumeric = recordnr;
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
    return this.mapJukeboxService.getAllJukeboxSelections().then(data => {
      this.mapJukeboxSelections = data;
      });
  }

  archiveSelection(jbselection) {
    return this.mapJukeboxService.archiveSelection(jbselection.Id).then(data => {
      this.loadData();
    });
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
