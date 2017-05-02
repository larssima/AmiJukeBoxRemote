export class App {
  configureRouter(config, router) {
    config.title = 'AMI Jukebox';
    config.map([
      { route: ['', 'import'], name: 'selections',      moduleId: 'views/import',      nav: true, title: 'Selections' },
    ]);

    this.router = router;
  }
}
