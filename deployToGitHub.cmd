
git config --global user.email "services@fifthweek.com"
git config --global user.name "%APPVEYOR_REPO_COMMIT_AUTHOR%"

mkdir "%APPVEYOR_BUILD_FOLDER%\temp_dist"
mkdir "%APPVEYOR_BUILD_FOLDER%\temp_dist_git_settings"

echo Cloning fifthweek-api-dist
git clone --branch=master https://%git_personal_access_token%:x-oauth-basic@github.com/fifthweek/fifthweek-api-dist.git "%APPVEYOR_BUILD_FOLDER%\temp_dist"

echo Preserving GIT settings for dist repo
xcopy /h "%APPVEYOR_BUILD_FOLDER%\temp_dist\.git" "%APPVEYOR_BUILD_FOLDER%\temp_dist_git_settings\.git\"

echo Clearing files
rmdir /s /q "%APPVEYOR_BUILD_FOLDER%\temp_dist*"
mkdir "%APPVEYOR_BUILD_FOLDER%\temp_dist"

echo Copying new files to dist repo
xcopy /h "%APPVEYOR_BUILD_FOLDER%\dist" "%APPVEYOR_BUILD_FOLDER%\temp_dist\"

echo Re-apply GIT settings for dist repo
xcopy /h "%APPVEYOR_BUILD_FOLDER%\temp_dist_git_settings\.git" "%APPVEYOR_BUILD_FOLDER%\temp_dist\.git\"

echo Changing directory
cd "%APPVEYOR_BUILD_FOLDER%\temp_dist"

echo Pushing new files to fifthweek-api-dist
git add -A
git commit -m "%APPVEYOR_REPO_COMMIT_MESSAGE% - %APPVEYOR_BUILD_NUMBER%"
git push https://%git_personal_access_token%:x-oauth-basic@github.com/fifthweek/fifthweek-api-dist.git %APPVEYOR_REPO_BRANCH%
