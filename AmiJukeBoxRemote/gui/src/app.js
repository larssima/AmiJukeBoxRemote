export class App {
  configureRouter(config, router) {
    config.title = 'AMI Jukebox';
    config.map([
      { route: ['', 'selection'], name: 'selections',      moduleId: 'views/selection',      nav: true, title: 'Selections' },
    ]);

    this.router = router;
  }
}
