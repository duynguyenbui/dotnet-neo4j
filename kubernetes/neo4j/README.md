### **Install a Neo4j Standalone Instance in Kubernetes**

**Introduction**

This guide outlines the steps to install a standalone instance of Neo4j, a popular graph database, within a Kubernetes
environment using Helm charts.

**Prerequisites**

- A functional Kubernetes cluster with Helm 3
  installed ([https://helm.sh/docs/intro/install/](https://helm.sh/docs/intro/install/))
- Basic understanding of Kubernetes concepts (namespaces, deployments, etc.)

**Installation Steps**

1. **Create a Namespace:**
   ```bash
   kubectl create namespace neo4j
   ```
   This command creates a dedicated namespace named `neo4j` for your Neo4j instance.

2. **Set the Current Namespace:**
   ```bash
   kubectl config set-context --current --namespace=neo4j
   ```
   This ensures all subsequent Kubernetes commands operate within the newly created `neo4j` namespace.

3. **Install Neo4j Chart:**
   ```bash
   helm install my-neo4j-release neo4j/neo4j --namespace neo4j -f my-neo4j.values.yaml
   ```
    - `my-neo4j-release`: Choose a descriptive release name (lowercase alphanumeric characters, "-", or ".").
    - `neo4j/neo4j`: Reference the official Neo4j Helm chart.
    - `-f my-neo4j.values.yaml`: Specify a custom values file (optional) to override default configurations.

   Replace `my-neo4j.values.yaml` with the actual path to your values file if you're using one.

4. **Monitor Rollout:**
   ```bash
   kubectl rollout status --watch --timeout=600s statefulset/my-neo4j-release
   ```
   This command displays the progress of the Neo4j deployment. Wait until the status changes to "deployed" before
   proceeding.
5. **Uninstall Neo4j Helm deployment:**
    ```bash
      helm uninstall my-neo4j-release
      ```
   Example output: release "my-neo4j-release" uninstalled
6. **Fully remove all the data and resources:**
    ```bash
   kubectl delete pvc --all --namespace neo4j
   ```