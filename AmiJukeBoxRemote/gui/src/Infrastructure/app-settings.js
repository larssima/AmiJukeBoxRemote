export class AppSettings {
    volUrl = '';

    constructor() {
        // API runs as a standalone service on port 8083
        this.baseUrl = `${window.location.protocol}//${window.location.hostname}:8083/`;
        this.api = this.baseUrl + 'api/';
    }
}

export class Busy {
    active = 0;

    on() { this.active++; }
    off() { this.active--; }
}