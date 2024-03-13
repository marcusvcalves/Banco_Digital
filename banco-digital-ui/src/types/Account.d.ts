import { Client } from "./Client";

export type Account = {
    id: number,
    number: string,
    balance: number,
    accountType: string,
    creationDate: string,
    clientId: number
    client: Client
  }