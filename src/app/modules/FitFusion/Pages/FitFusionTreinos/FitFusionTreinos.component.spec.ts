/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { FitFusionTreinosComponent } from './FitFusionTreinos.component';

describe('FitFusionTreinosComponent', () => {
  let component: FitFusionTreinosComponent;
  let fixture: ComponentFixture<FitFusionTreinosComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FitFusionTreinosComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FitFusionTreinosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
