Please read this before working on the project !

> GIT <

-- Branch --
> Workflow
	- Master : the latest approved version, no bugs, only merge approved features
	- Dev : the main work branch, used for merge and test every changes
	- [name] (others) : create a new branch for each features
	- Merge : to merge commit from the other repo


> Commit convention
commit msg :		[keyword(s)] [TA]? : [title]
commit description :	short description (wich features/bug/ect..)   // Optionnal

keywords :
- feat	: new feature
- fix	: fix a bug
- perf	: optimization (load/fps/importation/huge code improvment)
- doc	: add a new doc or comments in code
- refactor : editing code for make it cleaner
- LD 	: editing scene/prefabs and integration
- debug : tools or display for debugging
- asset	: import new in game assets
- import : import other files (different that doc or assets) like code packages

- TA	: tested and approved by the developper. Only for keywords : feat/LD

Exemple : [feat/fix] PlayerController + fix index error NbrPlayers
	  [LD/assets] Player Prefabs + Import player animations


-- UNITY --
