import os
from watchdog.observers import Observer
from watchdog.events import FileSystemEventHandler
import subprocess

class SyncHandler(FileSystemEventHandler):
    def on_created(self, event):
        if not event.is_directory and ".git" not in event.src_path:
            print(f"New file detected: {event.src_path}")
            self.git_sync(event.src_path, action="add")

    def on_deleted(self, event):
        if not event.is_directory and ".git" not in event.src_path:
            print(f"File deleted: {event.src_path}")
            self.git_sync(event.src_path, action="delete")

    def git_sync(self, path, action):
        try:
            if action == "add":
                subprocess.run(["git", "add", "-A"], check=True)
            elif action == "delete":
                relative_path = os.path.relpath(path)
                subprocess.run(["git", "rm", relative_path], check=True)
            subprocess.run(["git", "commit", "-m", f"Sync changes for {path}"], check=True)
            subprocess.run(["git", "push"], check=True)
        except subprocess.CalledProcessError as e:
            print(f"Error during Git operation: {e}")

if __name__ == "__main__":
    path_to_watch = "./"  # Replace with your directory
    event_handler = SyncHandler()
    observer = Observer()
    observer.schedule(event_handler, path_to_watch, recursive=True)
    observer.start()
    print("Monitoring directory for changes... Press Ctrl+C to stop.")
    try:
        while True:
            pass
    except KeyboardInterrupt:
        observer.stop()
    observer.join()




