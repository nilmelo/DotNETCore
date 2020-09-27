/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { MyEventService } from './MyEvent.service';

describe('Service: MyEvent', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [MyEventService]
    });
  });

  it('should ...', inject([MyEventService], (service: MyEventService) => {
    expect(service).toBeTruthy();
  }));
});
