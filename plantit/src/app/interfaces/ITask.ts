import { IZone } from "./IZone";

export enum Status{Todo, In_Progress, Done}
export enum Priority{Low, Medium, High}
export interface ITask{
    id: number;
    name: string;
    description: string;
    WorkScheduleId:number;
    ZoneId:number;
    scheduledStart:Date;
    scheduledStop:Date;
    actualStart:Date;
    actualStop:Date;
    Zone: IZone;
    Priority: Priority;
    status: Status;
  }