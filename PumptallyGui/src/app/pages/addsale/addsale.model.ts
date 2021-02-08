export class Sales {
    public id: number = 0;
    public dateofSale: string
    public productId: number = 0;
    public productName: string;
    public qty: number = 0
    public qtyPurchased: number = 0
    public rate: number = 0
    public amount: number = 0
    public shift: number = 0
    public createdBy: number = 0
    public createdOn: Date
    public updatedBy: number = 0
    public updatedOn: Date
    public inedit: boolean = false;
    public userId: number = 0

}

export class Voucher {
    public id: number = 0;
    public date: string
    public description: string = ""
    public ammount: number = 0
    public shift: number = 0
    public createdBy: number = 0
    public createdOn: Date
    public updatedBy: number = 0
    public updatedOn: Date
    public inedit: boolean = false;
    public userId: number = 0
}

export class PumpBill {
    public id: number = 0;
    public date: string
    public description: string = ""
    public amount: number = 0
    public shift: number = 0
    public createdBy: number = 0
    public createdOn: Date
    public updatedBy: number = 0
    public updatedOn: Date
    public inedit: boolean = false;
    public userId: number = 0
}

export class BiscuitBill {
    public id: number = 0;
    public date: string
    public description: string = ""
    public amount: number = 0
    public shift: number = 0
    public createdBy: number = 0
    public createdOn: Date
    public updatedBy: number = 0
    public updatedOn: Date
    public inedit: boolean = false;
    public userId: number = 0
}

export class HSDMeter {
    public id: number = 0
    public date: Date
    public hsdmeter1: number = 0
    public hsdmeter2: number = 0
    public hsdmeteropening1: number = 0
    public hsdmeteropening2: number = 0
    public hsddip1Opening: number = 0
    public hsddip1: number = 0
    public hsdtotal1: number = 0
    public hsdmeter3: number = 0
    public hsdmeter4: number = 0
    public hsddip2Opening: number = 0
    public hsddip2: number = 0
    public hsdtotal2: number = 0
    public shift: number = 0
}

export class MSMeter {
    public id: number = 0
    public date: Date
    public msMeter1: number = 0
    public msMeter2: number = 0
    public msDip1: number = 0
    public msTotal1: number = 0
    public msMeter3: number = 0
    public msMeter4: number = 0
    public msDip2: number = 0
    public msTotal2: number = 0
    public shift: number = 0
}