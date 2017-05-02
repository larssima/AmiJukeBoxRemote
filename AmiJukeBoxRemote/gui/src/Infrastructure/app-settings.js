export class AppSettings {
    volUrl = '';

    constructor() {
        if (window.location.hostname === 'localhost') {
            // Config for development
            this.baseUrl = 'http://localhost/rmidpsimport/';
        } else {
            // Config for test and prod
            this.baseUrl = '/';
        }
        this.api = this.baseUrl + 'api/';
    }
}

export class Busy {
    active = 0;

    on() { this.active++; }
    off() { this.active--; }
}