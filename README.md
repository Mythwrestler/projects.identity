# Deverloper Certificates



## Developer Certificates
In order to run this application as a developer container we need to create a self signed certificate, and install it.

### Step 1: Create And Install Certificate In WSL Environment

Create deployment and build directories
```bash
mkdir ~/buildcert
mkdir ~/.aspnet/https
```

Move into Build directory
```bash
cd ~/buildcert
```

Create development certificate configuration file *localhost.conf*
```text
[req]
prompt                  = no
default_bits            = 2048
distinguished_name      = subject
req_extensions          = req_ext
x509_extensions         = x509_ext
[subject]
commonName              = localhost
[req_ext]
basicConstraints        = critical, CA:true
subjectAltName          = @alt_names
[x509_ext]
basicConstraints        = critical, CA:true
keyUsage                = critical, keyCertSign, cRLSign, digitalSignature,keyEncipherment
extendedKeyUsage        = critical, serverAuth
subjectAltName          = critical, @alt_names
1.3.6.1.4.1.311.84.1.1  = ASN1:UTF8String:ASP.NET Core HTTPS development certificate
[alt_names]
DNS.`1                   = localhost
DNS.2                   = 127.0.0.1
```

Generate certificate and key
```bash
openssl req -x509 -nodes -days 365 -newkey rsa:2048 \
-keyout localhost.key \
-out localhost.crt \
-config localhost.conf
```

Add certificate to trusted certs
```bash
sudo cp localhost.crt /usr/local/share/ca-certificates
sudo update-ca-certificates
```

Verify certificate is trusted.
```bash
openssl verify localhost.crt
```
```text
localhost.crt: OK
```

Generate pfx certificate
```bash
openssl pkcs12 -export -out localhost.pfx -inkey localhost.key -in localhost.crt
```

Install certificate for dotnet mounting
```bash
cp localhost.pfx ~/.aspnet/https/localhost.pfx
```

Remove buildcert directory
```bash
rm -R ~/buildcert
```


### Step 2: Install Certificate In Windows

Copy the certificate into an equivalent directory in you windows user directory.

(From WSL Environment)
```bash
cp ~/.aspnet/https/localhost.pfx /mnt/c/Users/<USERNAME>/.aspnet/https
```

Open up the containing directory and right click local.pfx.

Select install pfx

Follow the prompts to install the certificate into Trusted Certificate Authorities
