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

    getAllArchivedJukeboxSelections(){
        return this.baseRepo.get('amijukebox/getallarchivedjukeboxselections').then(data=> {return data;});
    }

    loginToSpotify()
    {
        return this.baseRepo.get('amijukebox/spotifylogin').then(data=> {return data;});
    }

    playSongOnSpotify(artist,songtitle,que)
    {
        let data = {
            Artist: artist,
            SongTitle: songtitle,
            Que: que
        };          
        return this.baseRepo.put('amijukebox/playsongonspotify',data)
    }

    playSongOnJukebox(jbLetter,jbNumber)
    {
        let data = {
            JbLetter: jbLetter,
            JbNumber: jbNumber
        }
        return this.baseRepo.get('amijukebox/playsongonjukebox',data)
    }
}