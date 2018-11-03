import { AuthGuard } from './auth.guard';
import { Directive, Input, TemplateRef, ViewContainerRef } from '@angular/core';

@Directive({
  selector: '[appHasAnyAuthority]'
})
export class HasAnyAuthorityDirective {

  constructor(
    private templateRef: TemplateRef<any>,
    private viewContainer: ViewContainerRef,
    private authenticate: AuthGuard
  ) {
  }

  @Input()
  set appHasAnyAuthority(val) {
    if (val === this.authenticate.hasAnyAuthority().group) {
      this.viewContainer.createEmbeddedView(this.templateRef);
    } else {
      this.viewContainer.clear();
    }
  }
}
