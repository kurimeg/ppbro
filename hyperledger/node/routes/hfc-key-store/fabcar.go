package main

import (
        "bytes"
        "encoding/json"
        "fmt"
        "time"
        "github.com/hyperledger/fabric/core/chaincode/shim"
        sc "github.com/hyperledger/fabric/protos/peer"
)

type Profile struct {
        Address   string `json:"Address"`
        OrgAddress string `json:"OrgAddress"`
        MySign string `json:"MySign"`
        OrgSign string `json:"OrgSign"`
        Proof []Proof
}

type Proof struct {
        Id string `json:"Id"`
        Value string `json:"Value"`
        OrgSign string `json:"OrgSign"`
        DateTime time.Time `json:"DateTime"`
}

type Organization struct {
        Address string `json:"Address"`
        Name string `json:"Name"`
        Pubkey string `json:"Pubkey"`
}

type SmartContract struct {
}

func (s *SmartContract) Init(APIstub shim.ChaincodeStubInterface) sc.Response {
        return shim.Success(nil)
}

func (s *SmartContract) Invoke(APIstub shim.ChaincodeStubInterface) sc.Response {
        function, args := APIstub.GetFunctionAndParameters()
        if function == "initProfile" {
                return s.initProfile(APIstub, args)
        }else if function == "getProfile" {
                return s.getProfile(APIstub,args)
        }else if function == "issueProof" {
                return s.issueProof(APIstub,args)
        }else if function == "getProfileByOrgAddress" {
                return s.getProfileByOrgAddress(APIstub,args)
        }else if function == "signProfile" {
                return s.signProfile(APIstub,args)
        }else if function == "initOrg" {
                return s.initOrg(APIstub,args)     
        }else if function == "getOrg" {
                return s.getOrg(APIstub,args)         
        }else if function == "initLedger" {
                return s.initLedger(APIstub)
        }

        return shim.Error("Invalid Smart Contract function name.")
        return shim.Success(nil)
}

func (s *SmartContract) initProfile(APIstub shim.ChaincodeStubInterface, args [] string) sc.Response {
        var address, orgAddress, mySign, orgSign string
        proof := []Proof{}

        address = args[0]
        orgAddress = args[1]
        mySign = args[2]
        orgSign = args[3]
        
        //error check
        profileAsbytes, _ := APIstub.GetState(address)
        if(profileAsbytes != nil){
                return shim.Error("already exist")
        }

        //putstate
        profile := Profile{}
        profile.Address = address
        profile.OrgAddress = orgAddress
        profile.MySign = mySign
        profile.OrgSign = orgSign
        profile.Proof = proof

        profileAsBytes, _ := json.Marshal(profile)

        APIstub.PutState(address, profileAsBytes)
        return shim.Success(nil)
}

func (s *SmartContract) signProfile(APIstub shim.ChaincodeStubInterface, args [] string) sc.Response {
        var address, sign string
        var err error
        address = args[0]
        sign = args[1]
        
        profileAsbytes, err := APIstub.GetState(address)
        if err != nil {
                return shim.Error(err.Error())
        }
        if profileAsbytes == nil {
                return shim.Error("not found profile")
        }
        profile := Profile{}
        json.Unmarshal(profileAsbytes, &profile)

        profile.OrgSign = sign
        updatedProfilebytes, _ := json.Marshal(profile)
        APIstub.PutState(address, updatedProfilebytes)

        return shim.Success(nil)
}

func (s *SmartContract) getProfile(APIstub shim.ChaincodeStubInterface, args [] string) sc.Response {
        var address string
        var err error
        address = args[0]
        profileAsbytes, err := APIstub.GetState(address)
        if err != nil {
                return shim.Error(err.Error())
        }
        return shim.Success(profileAsbytes)
}

func (s *SmartContract) issueProof(APIstub shim.ChaincodeStubInterface, args [] string) sc.Response {
        var address, id, value, orgSign string
        var err error
        address = args[0]
        id = args[1]
        value = args[2]
        orgSign = args[3]
        
        nowtime := time.Now()
        
        profileAsbytes, err := APIstub.GetState(address)
        if err != nil {
                return shim.Error(err.Error())
        }
        if profileAsbytes == nil {
                return shim.Error("not found profile")
        }

        profile := Profile{}
        json.Unmarshal(profileAsbytes, &profile)
        profile.Proof = append(profile.Proof, Proof{Id: id,Value: value,OrgSign: orgSign,DateTime: nowtime})

        updatedProfilebytes, _ := json.Marshal(profile)
        APIstub.PutState(address, updatedProfilebytes)
        
        return shim.Success(nil)
}

func (s *SmartContract) getProfileByOrgAddress(APIstub shim.ChaincodeStubInterface, args [] string) sc.Response {
        var orgAddress, query string
        var err error
        orgAddress = args[0]

        query = "{\"selector\": {\"OrgAddress\": \"" + orgAddress + "\"}}"

        resultsIterator, err := APIstub.GetQueryResult(query)
        if err != nil {
                return shim.Error(err.Error())
        }
        defer resultsIterator.Close()

        var buffer bytes.Buffer
        buffer.WriteString("[")
        bArrayMemberAlreadyWritten := false
        for resultsIterator.HasNext() {
                queryResponse, err := resultsIterator.Next()
                jsn, jerr := json.Marshal(queryResponse.Key)
                if jerr != nil {
                        return shim.Error(err.Error())
                }
                if err != nil {
                        return shim.Error(err.Error())
                }
                if bArrayMemberAlreadyWritten == true {
                        buffer.WriteString(",")
                }
                buffer.WriteString("{\"Key\":")
                buffer.WriteString(string(jsn))
                buffer.WriteString(", \"Record\":")
                buffer.WriteString(string(queryResponse.Value))
                buffer.WriteString("}")
                bArrayMemberAlreadyWritten = true
        }
        buffer.WriteString("]")
        return shim.Success(buffer.Bytes())
}

func (s *SmartContract) initOrg(APIstub shim.ChaincodeStubInterface, args [] string) sc.Response {
 
        org := Organization{}
        org.Address = "U9F1B20C8E6DD429A90C090BF39334934"
        org.Name = "Osaka University"
        org.Pubkey = "pubkey01"
        orgAsBytes, _ := json.Marshal(org)
        APIstub.PutState(org.Address, orgAsBytes)
        
        org.Address = "U8F1B20C8E6DD429A90C090BF39334934"
        org.Name = "Tokyo University"
        org.Pubkey = "pubkey02"
        orgAsBytes, _ = json.Marshal(org)
        APIstub.PutState(org.Address, orgAsBytes)
        
        org.Address = "U7F1B20C8E6DD429A90C090BF39334934"
        org.Name = "Okinawa University"
        org.Pubkey = "pubkey03"
        orgAsBytes, _ = json.Marshal(org)
        APIstub.PutState(org.Address, orgAsBytes)
        
        return shim.Success(nil)
}

func (s *SmartContract) getOrg(APIstub shim.ChaincodeStubInterface, args [] string) sc.Response {
        var query string
        var err error

        query = "{\"selector\":{\"Address\":{\"$in\":[\"U9F1B20C8E6DD429A90C090BF39334934\",\"U8F1B20C8E6DD429A90C090BF39334934\",\"U7F1B20C8E6DD429A90C090BF39334934\"]}}}"
        resultsIterator, err := APIstub.GetQueryResult(query)
        if err != nil {
                return shim.Error(err.Error())
        }
        defer resultsIterator.Close()

        var buffer bytes.Buffer
        buffer.WriteString("[")
        bArrayMemberAlreadyWritten := false
        for resultsIterator.HasNext() {
                queryResponse, err := resultsIterator.Next()
                jsn, jerr := json.Marshal(queryResponse.Key)
                if jerr != nil {
                        return shim.Error(err.Error())
                }
                if err != nil {
                        return shim.Error(err.Error())
                }
                if bArrayMemberAlreadyWritten == true {
                        buffer.WriteString(",")
                }
                buffer.WriteString("{\"Key\":")
                buffer.WriteString(string(jsn))
                buffer.WriteString(", \"Record\":")
                buffer.WriteString(string(queryResponse.Value))
                buffer.WriteString("}")
                bArrayMemberAlreadyWritten = true
        }
        buffer.WriteString("]")
        return shim.Success(buffer.Bytes())
}

//init
func (s *SmartContract) initLedger(APIstub shim.ChaincodeStubInterface) sc.Response {
        return shim.Success(nil)
}

func main() {
        // Create a new Smart Contract
        err := shim.Start(new(SmartContract))
        if err != nil {
                fmt.Printf("Error creating new Smart Contract: %s", err)
        }
}