from diagrams.aws.compute import ECS

def diag2():
    box = ECS("Persisted Outbox")
    worker = ECS("Outbox Worker")

    box >> worker

    return box, worker