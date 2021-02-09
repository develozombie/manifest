# Manifest

Manifest is a project to help you generate the Azure AD App Roles Manifest to register your own application roles, this is something that will help you generate the exact required JSON structure as specified in [Azure Docs](https://docs.microsoft.com/en-us/azure/active-directory/develop/reference-app-manifest#manifest-reference) this project has been designed to run as Azure Function, install instructions bellow.

## Installation

Use VS Code and install the [Azure Function](https://marketplace.visualstudio.com/items?itemName=ms-azuretools.vscode-azurefunctions) extension.
### For Windows
Download [Azure Function Core Tools 64 bits](https://go.microsoft.com/fwlink/?linkid=2135274)
### For Linux 
Next steps are for Ubuntu
```bash
curl https://packages.microsoft.com/keys/microsoft.asc | gpg --dearmor > microsoft.gpg
sudo mv microsoft.gpg /etc/apt/trusted.gpg.d/microsoft.gpg
sudo sh -c 'echo "deb [arch=amd64] https://packages.microsoft.com/repos/microsoft-ubuntu-$(lsb_release -cs)-prod $(lsb_release -cs) main" > /etc/apt/sources.list.d/dotnetdev.list'
```

## Usage

Pass the roles you want to define using a JSON Array using a POST request ant defining Content-Type:application/json

```json
[
	{
	"RoleName":"admin",
	"RoleDescription": "Este rol tiene la función de administrar el acceso a los usuarios de App.User.*"
},
{
	"RoleName":"lector de mensajes",
	"RoleDescription": "este rol tiene la funcion de bla bla bla bla"
}
]
```
The result will be shown also in JSON
```json
[
  {
    "allowedMemberTypes": [
      "User"
    ],
    "displayName": "admin",
    "id": "42f3dedb-b96c-419a-bbd8-3122dce4daf5",
    "isEnabled": true,
    "description": "Este rol tiene la función de administrar el acceso a los usuarios de App.User.*",
    "value": "admin"
  },
  {
    "allowedMemberTypes": [
      "User"
    ],
    "displayName": "lector de mensajes",
    "id": "267071eb-8b82-4699-882f-5892eca2a08e",
    "isEnabled": true,
    "description": "este rol tiene la funcion de bla bla bla bla",
    "value": "lector_de_mensajes"
  }
]
```
In the Manifest you will find, around line 8, the "appRoles":[] definition will be empty, inside square brackets paste the output of the API. The result should look like this:
```json
{
	"id": "",
	"acceptMappedClaims": null,
	"accessTokenAcceptedVersion": null,
	"addIns": [],
	"allowPublicClient": false,
	"appId": "0ad6689e-f680-4cbb-ba57-0fd00b53d89e",
	"appRoles": [
        {
            "allowedMemberTypes": [
            "User"
            ],
            "displayName": "admin",
            "id": "42f3dedb-b96c-419a-bbd8-3122dce4daf5",
            "isEnabled": true,
            "description": "Este rol tiene la función de administrar el acceso a los usuarios de App.User.*",
            "value": "admin"
        },
        {
            "allowedMemberTypes": [
            "User"
            ],
            "displayName": "lector de mensajes",
            "id": "267071eb-8b82-4699-882f-5892eca2a08e",
            "isEnabled": true,
            "description": "este rol tiene la funcion de bla bla bla bla",
            "value": "lector_de_mensajes"
        }
    ],
	"oauth2AllowUrlPathMatching": false,
	"createdDateTime": "2017-05-12T22:14:12Z",
	"disabledByMicrosoftStatus": null,
	"groupMembershipClaims": null,
	"identifierUris": [
		""
	],
	"informationalUrls": {
		"termsOfService": null,
		"support": null,
		"privacy": null,
		"marketing": null
	},
	"keyCredentials": [],
	"knownClientApplications": [],
	"logoUrl": "",
	"logoutUrl": "https://demo.azurewebsites.net",
	"name": "Demo",
	"oauth2AllowIdTokenImplicitFlow": true,
	"oauth2AllowImplicitFlow": false,
	"oauth2Permissions": [
		{
			"adminConsentDescription": "Allow the application to access Demo on behalf of the signed-in user.",
			"adminConsentDisplayName": "Access Demo",
			"id": "",
			"isEnabled": true,
			"lang": null,
			"origin": "Application",
			"type": "User",
			"userConsentDescription": "Allow the application to access Demo on your behalf.",
			"userConsentDisplayName": "Access Demo",
			"value": "user_impersonation"
		}
	],
	"oauth2RequirePostResponse": false,
	"optionalClaims": null,
	"orgRestrictions": [],
	"parentalControlSettings": {
		"countriesBlockedForMinors": [],
		"legalAgeGroupRule": "Allow"
	},
	"passwordCredentials": [
		{
			"customKeyIdentifier": "",
			"endDate": "2299-12-31T05:00:00Z",
			"keyId": "",
			"startDate": "2017-05-12T22:22:26.2665056Z",
			"value": null,
			"createdOn": null,
			"hint": null,
			"displayName": null
		}
	],
	"preAuthorizedApplications": [],
	"publisherDomain": null,
	"replyUrlsWithType": [
		{
			"url": "https://demo.azurewebsites.net",
			"type": "Web"
		}
	],
	"requiredResourceAccess": [
		{
			"resourceAppId": "",
			"resourceAccess": [
				{
					"id": "",
					"type": "Scope"
				}
			]
		}
	],
	"samlMetadataUrl": null,
	"signInUrl": "https://demo.azurewebsites.net",
	"signInAudience": "AzureADMyOrg",
	"tags": [],
	"tokenEncryptionKeyId": null
}
```
## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change. For direct contact please reach us emailing joyapu@microsoft.com and cefong@microsoft.com

Please make sure to update tests as appropriate.

## License
[MIT](https://choosealicense.com/licenses/mit/)