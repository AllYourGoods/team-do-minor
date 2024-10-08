# Setting Up the Azure Environment for the Pipeline

Follow these steps to connect your pipeline to the Azure environment successfully.

## Step 1: Sign Up for an Azure Portal

- Go to the [Azure Portal](https://portal.azure.com) and create an account if you don’t already have one.
- Make sure to use your student email address to access the **Azure for Students** benefits.

## Step 2: Connect to Receive Student Benefits

- Follow the instructions to activate the **Azure for Students** subscription in your account.

## Step 3: Verify Your Subscription in the Azure Portal

- In the Azure portal, use the **Search bar** (located at the top of the page) and type `Subscriptions`.
- Open the **Subscriptions** blade.
- Check if `Azure for Students` is listed.
- Note down the **Subscription ID**, as it will be needed in subsequent steps.

## Step 4: Install the Azure CLI

- Download and install the Azure CLI from the [official Azure CLI installation page](https://learn.microsoft.com/cli/azure/install-azure-cli).
- Follow the instructions for your specific operating system.

## Step 5: Log in to Azure CLI

After installing the Azure CLI, open a terminal and run the following command:

```bash
az login
```

- Log in using the same account that you used for the Azure portal.
- During the login process, ensure that you select the Azure for Students subscription.

## Step 6: Create an App Registration

To allow GitHub Actions to authenticate and interact with Azure, create an App Registration using the Azure CLI:

```bash
az ad sp create-for-rbac --name "github-actions-sp" --role Owner --scopes /subscriptions/<your-subscription-id> --sdk-auth
```

- Replace <your-subscription-id> with the Subscription ID you noted earlier.
- This command will create a service principal and provide you with a JSON object containing credentials.

## Step 7:  Save the JSON Output as a GitHub Secret

- Copy the entire JSON output from the previous step.
- Go to your GitHub repository.
- Navigate to Settings > Secrets and variables > Actions.
- Click on New Repository Secret.
- Set the name to `AZURE_CREDENTIALS` and paste the JSON output as the value.

## Step 8: Set up the Database Password

- Make sure that another GitHub Actions secret is created with the name `SQL_ADMIN_PASSWORD`.
- This password will be used to access the Azure SQL Database.

## Step 9:  Handling Errors During Deployment

If the pipeline gives an error stating that the application already exists, follow these steps:

- Open the deploy-to-x.yml file that generated the error.
- Modify the environment variable to use a different instance. For example, change:

```bash 
environment: test1
```
to

```bash 
environment: test2
```

- Save the file and recommit to retrigger the pipeline.

This change will create a new instance and should resolve the error.


