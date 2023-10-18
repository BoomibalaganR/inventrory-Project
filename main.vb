Module VBModule 
public class inventory_class 
    public iteamID() as integer = {101,102,103,104,105} 
    public description ()  as String = {"Rice           ","sugar          ","salt           ","pen            ","pencil         "} 
    public pricePerUnit() as integer = {10,10,10,5,5} 
    public TaxIteam() as integer = {103,102}
   
   public function displayProduct()  as String 
     
       console.writeline("*******************************************")
       console.writeline("iteamId    description            price") 
       console.writeline("*******************************************")
       dim i as integer = 0
       for i = 0 to 4  
           console.writeline(iteamId(i) &"      "& description(i)& "          " & pricePerUnit(i))
       next 
       console.writeline("*******************************************") 
       
     console.writeline("if you purchase any iteam say 'yes' otherwise 'No' ")
     return console.Readline() 
   end function  
   
   public function isTaxable( byval purchaseID as integer) as integer 
       dim iteam as integer 
       
       
       for each iteam in TaxIteam 
           if(iteam = purchaseID) then 
               return 1
           end if
       next  
       
       return 0
   end function

    public function getPricePerUnit(byval purchaseId as integer) as integer
        dim i as integer = 0  
        
        for i = 0 to iteamID.length-1 
            if(iteamID(i) = purchaseId) then 
                return i
            end if 
        next
    end Function

end class  
'**************************** 

public class Bill: inherits inventory_class 
    dim i as integer = 0  
    dim totalAmount as integer = 0  
    dim ID as integer 
    dim custName as string 
    dim taxRate as integer = 0
    
    dim iteamName() as String
    dim UnitPrice() as integer  
    dim amount() as integer  
    
    'constructor  
    public sub New(byval id as integer,byval Name as string) 
        ID = id 
        custName = Name 
        console.writeline("inside constructor: " & ID &" "&custName)
    end sub
    
    public sub billGenerate(byval purchaseId() as integer, byval quantity() as integer)  
        
        redim UnitPrice(purchaseId.length-1) 
        redim amount(purchaseId.length-1) 
        redim iteamName(purchaseId.length-1)   
        
        dim Index as integer
        
        
        for i = 0 to purchaseId.length-1  
            
            Index =  getPricePerUnit(purchaseId(i)) 
            
            unitPrice(i) = pricePerUnit(Index) 
            iteamName(i) = description(Index) 
            
           if(isTaxable(purchaseId(i)) = 1) then 
                taxRate += 0.1 * UnitPrice(i)*quantity(i) 
                amount(i) =  (quantity(i) * UnitPrice(i))  
                totalAmount+=amount(i)
            else 
                amount(i) = UnitPrice(i) * quantity(i)  
                totalAmount+=amount(i)
            end if  
        Next 
         
      
        billDisplay()  ' display the bill
    end Sub
    
    public sub billDisplay()   
         console.writeline("-------------------------------------------------")
         console.writeline() 
         console.writeline("                  Inventory Bill") 
         console.writeline("                                 Name    : " &custName)   
         console.writeline("                                 custID  : " &ID+1)
         console.writeline("-------------------------------------------------") 
         console.writeline("SNo:    Iteam Name:    Unit Price:      Total:    " )
         console.writeline("-------------------------------------------------")  
         
        for i = 0 to  iteamName.length-1 
            console.writeline(" "&i+1 & "        " &iteamName(i) &"Rs"&unitPrice(i)&"           "&amount(i))
        Next 
        console.writeline("--------------------------------------------------")  
        console.writeline("                   Total:               Rs"&totalAmount)  
        console.writeline("                   Tax  :               Rs"&taxRate) 
        console.writeline("--------------------------------------------------")  
        console.writeline("TotalAmount:                            Rs"&totalAmount+taxRate) 
        console.writeline("--------------------------------------------------")    
        console.writeline()
        console.writeline("                *****Thank You*****")  
        console.writeline()
        
    end Sub
end Class
'************************************

    Sub Main() 
       
        dim purchaseId() as integer
        dim quantity() as integer 
        dim totalProduct as integer 
        dim i as integer = 0
          dim obj as new inventory_class()  
        dim customer(2) as Bill 'array of object
        dim id as integer = 0
        dim custName as string 
        
           
        while(1)
            
            if(obj.displayProduct() = "yes") then    
                 
                  
                console.writeline("enter your name") 
                custName = console.Readline 
                
               customer(id) = new Bill(id,custName) 'create object 
                   console.writeline("enter how many product purchase")
                   totalProduct = console.Readline()  
                   
                   redim purchaseId(totalProduct-1)
                   redim quantity(totalProduct-1)
                   
                   
                  for  i = 0 to totalproduct-1  
                      console.writeline()
                       console.writeline("enter your iteamId ")
                        purchaseId(i) = console.Readline()
                        console.writeline("enter your quantity") 
                        quantity(i) = console.Readline()
                   Next 
                 
               customer(id).billGenerate(purchaseId, quantity)  'function call for billGenerate 
               exit while 
            else 
                console.writeline("thank you...........!!") 
                exit while 
            end If
        end while
     console.writeline("enter your id: ") 
     dim p as integer = console.Readline() 
     customer(p-1).billDisplay

    End Sub 
End Module

