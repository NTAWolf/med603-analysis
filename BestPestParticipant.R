# TODO: Make histogram of all participants' thresholds
#               and standard deviations?
# TODO: Figure out a meaningful analysis of results
# TODO: Make data structure for gaze data
# TODO: Figure out a way to determine whether gaze was ok close to wanted positions

# Per-trial: See max gaze deviation after lo-pass filter (filter out gaze tracker noise).  If above threshold, discard trial.
# Per-participant: Calculate mean and standard deviation for thresholds



# PARTICIPANT has members id, T1, T2, T3, T4
MakeParticipant = function(id, T1, T2, T3, T4)
{
  participant = list("id" = id, "T1" = T1, "T2" = T2, "T3" = T3, "T4" = T4)
  return(participant)
}

GetTrialMatrix = function(participant)
{
  trials = ConcatenateTrials(participant$T1, participant$T2, participant$T3, participant$T4)
  return(trials)
}

GetThresholdsMatrix = function(participant)
{
  # Assuming threshold value is the last in each trial
  p = participant
  threshold = ConcatenateTrials(tail(p$T1, 1),tail(p$T2, 1),tail(p$T3, 1),tail(p$T4, 1))
  return(c("id" = p$id, threshold))
}

PlotStimulusValues = function(participant)
{
  trials = GetTrialMatrix(participant)
  
  plot(trials$Stimulus, 
       col = ifelse(trials$Trial == "T1" | trials$Trial == "T3", 'black', 'red'), 
       main=paste("Participant ", participant$id), 
       xlab="Observation", 
       ylab="Stimulus level",
       ylim=c(0,140))
}

PlotThresholdValues = function(participant)
{
  thresholds = GetThresholdsMatrix(participant)
  plot(thresholds$Stimulus, 
       col = 'black', 
       main=paste("Participant ", thresholds$id), 
       xlab="Trial", 
       ylab="Stimulus level threshold",
       ylim=c(0,140))
}

PlotThresholdValuesMultiple = function(participants)
{
  par(mfrow=c(length(participants),2))
  for(i in 1:length(participants))
  {
    PlotThresholdValues(participants[i])
  }
}

ConcatenateTrials = function(T1, T2, T3, T4)
{
  T1 = cbind(T1, "T1")
  T2 = cbind(T2, "T2")
  T3 = cbind(T3, "T3")
  T4 = cbind(T4, "T4")
  
  colnames(T1)[ncol(T1)] = "Trial"
  
  colnames(T2) = colnames(T1)
  colnames(T3) = colnames(T1)
  colnames(T4) = colnames(T1)
  
  output = rbind(T1, T2, T3, T4)
  #names(output) = ["Stimulus", "Response", "TrialNumber"]
  
  return (output)
}

GetThresholdStats = function(participant)
{
  thresholds = GetThresholdsMatrix(participant)$Stimulus
  meanVal = mean(thresholds)
  stdev = sd(thresholds)
  stats = list("Thresholds" = thresholds, "Mean" = meanVal, "SD"=stdev)
  return(stats)
}

ExtractIDs = function(...)
{
  ps = list(...)
  output = integer()
  output[1] = 12
  for(i in 1:length(ps))
  {
    output[i] = ps[i]$id
  }
  return(output)
}