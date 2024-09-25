# team-do-minor
Our teams name is TeamDOminor. The purpose of this repo's code is for the devops minor assignment

## Description
Probably for a backend service, but this is yet to be confirmed.


# Git workflow

## **Rules**

- The person who created the feature performs the merge after it has been approved.

# Steps git workflow

## **Start of feature - Assignee/Developer**

1. You pick up a user story by dragging it to the "In progress" column and assigning it to yourself;
2. **Create a branch** named feature/story-[ID], where you replace [ID] with the ID of the story on the board. Since this branch is created for one feature, we call this a feature-branch;
3. **You develop the story**. Make commits during your work, so don't wait until the user story is fully completed;
4. When you are done and have thoroughly tested, **drag the user story to the next column** on the board called "Review".

## **When Assignee is done with the feature**

**Assignee - When you are done with your feature**

- Push your feature branch (to your own remote branch!)
- Create a pull request
	- Link by putting `#{number}` e.g., `#3` in the description of your pull request. *You no longer need to do this if you create a branch from GitHub issues.*
- Drag card to “Test”

**Team member/Tester**

- A team member tests the code
	- There can be 2 scenarios:
		- Approved by tester:
			- Tester informs Assignee that it is good and lets the developer merge. Because Assignee/developer knows which code is crucial for their feature in case of a merge conflict.
			- Developer completes the final process
				- **Completion - Assignee/Developer**

					For this, take the following steps:
					a. Merge develop into your feature-branch and resolve any merge conflicts;
					b. Check if your work still functions correctly now that develop is merged into your feature branch.

					- Resolve any issues
					- When everything is fine, merge your feature-branch into develop.
					- Delete your feature branch, both locally and remotely.

					c. Drag the user story to the "Done" column.

			- Assignee deletes feature-branch both locally and remotely
			- Drags work to done.
		- Rejected by tester:
			- The tester explains why the test was rejected in the card, so the developer can find out what needs to be changed.
			- Assignee updates code.
				- Assignee/Developer starts again [at step 3](https://www.notion.so/Team-12-Gitflow-1-10c448205bbe806cafcef83d1f6d7738?pvs=21): developing the story

