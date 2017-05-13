import {inject} from 'aurelia-framework';
import {AppSettings} from '../infrastructure/app-settings';
import {BaseRepo} from '../infrastructure/baserepo';

@inject(AppSettings, BaseRepo)
export class MapJukeboxService {
    constructor(appSettings, baseRepo) {
        this.baseRepo = baseRepo;
    }

    cancelRecord(){
        return this.baseRepo.get('amijukebox/cancel').then(data=> {return data;});
    }

    createStrips(){
        return this.baseRepo.get('amijukebox/createstrips').then(data=> {return data;});
    }

    getAllJukeboxSelections(){
        return this.baseRepo.get('amijukebox/getalljukeboxselections').then(data=> {return data;});
    }



/*
    getAllRmIdpsActivities(){
        return this.baseRepo.get('rmidpsactivties/getallrmidpsactivities').then(data => {return data;});
    }

    getAllIdpsActivities(){
        return this.baseRepo.get('rmidpsactivties/getallidpsactivities').then(data => {return data;});
    }
    
    getAllIdpsFilterActivities(){
        return this.baseRepo.get('rmidpsactivties/getallidpsfilteractivities').then(data => {return data;});
    }
    

    getAllRmActivities(){
        return this.baseRepo.get('rmidpsactivties/getallrmactivities').then(data => {return data;});
    }

    insertRmIdpsActivity(rmactcode,rmdescription,idpsactcode,description,idpsdescription){
        let data = {
            RmActivityCode: rmactcode,
            IdpsActivityCode: idpsactcode,
            RmDescription: rmdescription,
            Description: description,
            IdpsDescription: idpsdescription
        };        
        return this.baseRepo.put('rmidpsactivties/insertrmidpsactivity', data)
    }

    insertIdpsActivity(idpsactcode,idpsdescription){
        let data = {
            IdpsActivityCode: idpsactcode,
            IdpsDescription: idpsdescription
        };        
        return this.baseRepo.put('rmidpsactivties/insertidpsactivity', data)
    }

    deleteRmIdpsActivity(activity){
        let data = {
            Id: activity.Id,
        };        
        return this.baseRepo.put('rmidpsactivties/deletermidpsactivity', data)
    }

    deleteIdpsActivity(activity){
        let data = {
            Id: activity.Id,
        };        
        return this.baseRepo.put('rmidpsactivties/deleteidpsactivity', data)
    }

    getAllRmIdpsEmployees(){
        return this.baseRepo.get('rmidpsactivties/getallrmidpsemployees').then(data => {return data;});
    }

    insertRmIdpsEmployee(rmempno,idpsempno){
        let data = {
            RmEmpNo: rmempno,
            IdpsEmpNo: idpsempno,
        };        
        return this.baseRepo.put('rmidpsactivties/insertrmidpsemployee', data)
    }

    deleteRmIdpsEmployee(employee){
        let data = {
            Id: employee.Id,
        };        
        return this.baseRepo.put('rmidpsactivties/deletermidpsemployee', data)
    }

    insertIdpsFilterActivity(idpsactcode,idpsdescription,description){
        let data = {
            IdpsActivityCode: idpsactcode,
            IdpsDescription: idpsdescription,
            Description: description
        };        
        return this.baseRepo.put('rmidpsactivties/insertidpsfilteractivity', data)
    }

    deleteIdpsFilterActivity(activity){
        let data = {
            Id: activity.Id,
        };        
        return this.baseRepo.put('rmidpsactivties/deleteidpsfilteractivity', data)
    }
  */  
    
    
}