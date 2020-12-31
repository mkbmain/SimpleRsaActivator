#Warning activation form will blow up with out generating a public key first


### To Use Simply call the RSASerialKey.MakeKey with paths in constutor




this will give you a private key and public key

the public key will be distrubted with in your application and you keep the private key

to generate a serial number get the machine
MachineFingerPrint


and use your private key on your server to generate a key from this you can then verify it in app to unlock paid featuers

e.g

            var keys = @"G:\keys\";
            RSASerialKey s = new RSASerialKey(keys+"Public.key",keys+"Private.key");  << private key will be empty on client

          var machine =  MachineFingerPrint.Value();

          var signed = s.SignData(machine);                                             << this will be done on server not client
          var verify = s.VerifyData(machine, File.ReadAllText(keys+"serial.txt"));       << this is all that needs to be done on client
          
          
         
# please also note its probably a good idea to change the finger print to include your apps name and maybe another detail or 2
