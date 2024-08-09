from diagrams.aws.compute import ECS
from diagrams.aws.compute import LambdaFunction
from diagrams.aws.storage import SimpleStorageServiceS3Bucket
from diagrams.onprem.queue import Kafka


def subsystem1():
    enpoint = LambdaFunction("Public Endpoint")
    bucket = SimpleStorageServiceS3Bucket("S3 Bucket")
    kafka = Kafka("stream")

    enpoint >> [bucket, kafka]
  
    return kafka