import { NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { DeleteMemesComponent } from './memes/delete-memes.component';
import { MemesService } from './memes/memes.service';
import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { HomeComponent } from './home.component';
import { SharedModule } from '../shared/shared.module';
import { AddMemesComponent } from './memes/add-memes.component';

@NgModule({
  imports: [
    SharedModule
  ],
  declarations: [
    HomeComponent,
    AddMemesComponent,
    DeleteMemesComponent
  ],
  entryComponents: [
    AddMemesComponent,
    DeleteMemesComponent
  ],
  providers: [
    MemesService
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class HomeModule {}
