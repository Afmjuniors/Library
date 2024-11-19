import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TypeOccurrenceRecordConfigurationComponent } from './type-occurrence-record-configuration.component';

describe('TypeOccurrenceRecordConfigurationComponent', () => {
  let component: TypeOccurrenceRecordConfigurationComponent;
  let fixture: ComponentFixture<TypeOccurrenceRecordConfigurationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TypeOccurrenceRecordConfigurationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TypeOccurrenceRecordConfigurationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
